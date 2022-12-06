namespace Magix.Domain.Board
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interface;
    using Interface.Board;
    using Interface.Board.Result;
    using Interface.NatureElements;
    using Result;

    public class Board : IBoard
    {
        public ITile[,] Tiles { get; private set; }

        public List<IPlayer> Players { get; private set; }

        public IPlayer CurrentPlayer { get; private set; }

        private Queue<IPlayer> _orderToPlay;

        private const int _size = 10;

        public Board(List<IPlayer> players)
        {
            Players = players;
            _orderToPlay = new Queue<IPlayer>(players);

            _createTiles();
            _setNextPlayerToPlay();
        }

        public void ApplyNatureElement(IWizard wizard, INatureElement natureElement, List<ITile> tiles)
        {
            _verifyWizardBelongsCurrentPlayer(wizard);

            foreach (ITile tile in tiles)
                tile.ApplyNatureElement(natureElement);

            wizard.RemoveRemainingActions(tiles.Count);

            _tryChangeCurrentPlayer();
        }

        public IMovementResult Move(IWizard wizard, List<ITile> tiles)
        {
            _verifyWizardBelongsCurrentPlayer(wizard);

            foreach (ITile tile in tiles)
                tile.NatureElement.ApplyEffect(wizard);

            wizard.RemoveRemainingActions(tiles.Count);

            wizard.Position = tiles.Last().Position;

            _tryChangeCurrentPlayer();

            return new MovementResult(
                tiles,
                wizard.Id.ToString(),
                true,
                string.Empty);
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

        public bool HasWizard(ITile tile)
        {
            IWizard wizard = GetWizard(tile);

            return wizard != null;
        }

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
