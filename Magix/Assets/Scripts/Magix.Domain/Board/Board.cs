namespace Magix.Domain.Board
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interface;
    using Interface.Board;
    using Interface.Board.Result;
    using Interface.NatureElements;
    using Interface.NatureElements.Result;
    using NatureElements;
    using Result;

    public class Board : IBoard
    {
        // TODO: Tiles should not be visible.
        public ITile[,] Tiles { get; private set; }

        public List<IPlayer> Players { get; private set; }

        public IPlayer CurrentPlayer { get; private set; }

        private readonly Queue<IPlayer> _orderToPlay;

        private const int _size = 10;

        public Board(List<IPlayer> players)
        {
            Players = players;
            _orderToPlay = new Queue<IPlayer>(players);

            _createTiles();
            _setNextPlayerToPlay();
        }

        // TODO: Pass positions instead of tiles.
        public ICastResult CastNatureElement(IWizard wizard, INatureElement natureElement, IList<ITile> tiles)
        {
            _verifyWizardBelongsCurrentPlayer(wizard);

            var allResults = new List<IMixResult>();

            foreach (ITile tile in tiles)
            {
                List<IMixResult> mixResults = tile.Mix(natureElement);

                allResults.AddRange(mixResults);
            }

            wizard.RemoveRemainingActions(tiles.Count);

            _tryChangeCurrentPlayer();

            return new CastResult(allResults);
        }

        public IAttackResult Attack(IWizard wizard, IPosition position)
        {
            IEnumerable<IWizard> allWizards = Players.SelectMany(p => p.Wizards);

            IWizard wizardInPosition = allWizards.FirstOrDefault(w => w.Position == position);

            var attackResult = new AttackResult(false, null, IAttackResult.NoWizardInPosition);

            if (wizardInPosition is not null)
            {
                IEffectResult effectResult = wizardInPosition.TakeDamage(IWizard.AttackDamage);

                attackResult = new AttackResult(true, effectResult);
            }

            return attackResult;
        }

        // TODO: Pass positions instead of tiles.
        public IMovementResult Move(IWizard wizard, IList<ITile> tiles)
        {
            _verifyWizardBelongsCurrentPlayer(wizard);

            var steps = new List<IStepResult>();

            foreach (ITile tile in tiles)
            {
                foreach (INatureElement natureElement in tile.NatureElements)
                {
                    if (natureElement.Blocking)
                    {
                        return new MovementResult(
                            new List<IStepResult>(),
                            false,
                            MovementResult.CantGoThroughBlockingElement);
                    }

                    IEffectResult effectResult = natureElement.ApplyElementEffect(wizard);

                    var step = new StepResult(tile, effectResult);

                    steps.Add(step);
                }
            }

            wizard.RemoveRemainingActions(tiles.Count);

            wizard.Position = tiles.Last().Position;

            _tryChangeCurrentPlayer();

            return new MovementResult(steps, true, string.Empty);
        }

        public List<INatureElement> GetNatureElementsToCast()
        {
            return new List<INatureElement> {new Fire(), new Water(), new Wind(),};
        }

        public List<ITile> GetAreaToMove(IWizard wizard)
        {
            return new List<ITile>();
        }

        // TODO: Review this algorithm.
        public List<ITile> GetPreviewPositionMoves(IWizard wizard, ITile objectiveTile)
        {
            var moves = new List<ITile>();

            IPosition wizardPosition = wizard.Position;

            int wizardPositionX = wizardPosition.X;
            int wizardPositionY = wizardPosition.Y;

            int objectivePositionX = objectiveTile.Position.X;
            int objectivePositionY = objectiveTile.Position.Y;

            if (wizardPositionX > objectivePositionX)
            {
                while (wizardPositionX > objectivePositionX)
                {
                    wizardPositionX--;

                    ITile nextTile = Tiles[wizardPositionX, wizardPositionY];

                    moves.Add(nextTile);
                }
            }
            else
            {
                while (wizardPositionX < objectivePositionX)
                {
                    wizardPositionX++;

                    ITile nextTile = Tiles[wizardPositionX, wizardPositionY];

                    moves.Add(nextTile);
                }
            }

            if (wizardPositionY > objectivePositionY)
            {
                while (wizardPositionY > objectivePositionY)
                {
                    wizardPositionY--;

                    ITile nextTile = Tiles[wizardPositionX, wizardPositionY];

                    moves.Add(nextTile);
                }
            }
            else
            {
                while (wizardPositionY < objectivePositionY)
                {
                    wizardPositionY++;

                    ITile nextTile = Tiles[wizardPositionX, wizardPositionY];

                    moves.Add(nextTile);
                }
            }

            moves = moves.Take(wizard.RemainingActions).ToList();

            return moves;
        }

        // TODO: Pass position instead of tile.
        public bool HasWizard(ITile tile)
        {
            IWizard wizard = GetWizard(tile);

            return wizard != null;
        }

        // TODO: Pass position instead of tile.
        public IWizard GetWizard(ITile tile)
        {
            IWizard wizardInTile = null;

            foreach (IPlayer player in Players)
            {
                foreach (IWizard wizard in player.Wizards)
                {
                    if (wizard.Position.Equals(tile.Position))
                        wizardInTile = wizard;
                }
            }

            return wizardInTile;
        }

        public bool BelongsToCurrentPlayer(IWizard wizard)
        {
            List<IWizard> wizards = CurrentPlayer.Wizards;

            return wizards.Contains(wizard);
        }

        private void _verifyWizardBelongsCurrentPlayer(IWizard wizard)
        {
            if (!BelongsToCurrentPlayer(wizard))
                throw new InvalidOperationException("Wizard does not belong to current player.");
        }

        private void _createTiles()
        {
            Tiles = new ITile[_size, _size];

            for (int line = 0; line < _size; line++)
            {
                for (int column = 0; column < _size; column++)
                {
                    var position = new Position(line, column);

                    Tiles[line, column] = new Tile(position);
                }
            }

            _configureAllAdjacentTiles();
        }

        private void _configureAllAdjacentTiles()
        {
            foreach (ITile tile in Tiles)
            {
                List<ITile> adjacent = _getAdjacentTiles(tile);

                tile.SetAdjacent(adjacent);
            }
        }

        private List<ITile> _getAdjacentTiles(ITile tile)
        {
            var adjacentTiles = new List<ITile>();

            List<(int, int)> adjacentIndexes = _calculateAdjacentIndexes(tile.Position);

            foreach ((int line, int column) in adjacentIndexes)
                adjacentTiles.Add(Tiles[line, column]);

            return adjacentTiles;
        }

        private List<(int, int)> _calculateAdjacentIndexes(IPosition position)
        {
            var adjacentIndexes = new List<(int, int)>();

            int line = position.X;
            int column = position.Y;

            if (line + 1 < _size)
                adjacentIndexes.Add((line + 1, column));

            if (line - 1 >= 0)
                adjacentIndexes.Add((line - 1, column));

            if (column + 1 < _size)
                adjacentIndexes.Add((line, column + 1));

            if (column - 1 >= 0)
                adjacentIndexes.Add((line, column - 1));

            return adjacentIndexes;
        }

        private void _tryChangeCurrentPlayer()
        {
            if (!CurrentPlayer.HasRemainingActions)
                _setNextPlayerToPlay();
        }

        private void _setNextPlayerToPlay()
        {
            IPlayer nextPlayer = _orderToPlay.Dequeue();
            _orderToPlay.Enqueue(nextPlayer);

            CurrentPlayer = nextPlayer;

            foreach (IWizard wizard in CurrentPlayer.Wizards)
                wizard.ResetRemainingActions();
        }
    }
}
