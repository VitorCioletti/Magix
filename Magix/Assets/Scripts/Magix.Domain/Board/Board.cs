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

        public Queue<IPlayer> Players { get; private set; }

        public IPlayer CurrentPlayer { get; private set; }

        public Dictionary<Guid, Dictionary<IWizard, IPosition>> WizardsPositions { get; private set; }

        private const int _size = 10;

        public Board(Dictionary<IPlayer, List<IPosition>> players)
        {
            WizardsPositions = new Dictionary<Guid, Dictionary<IWizard, IPosition>>();
            Players = new Queue<IPlayer>(players.Keys.ToList());

            foreach (IPlayer player in Players)
                WizardsPositions[player.Id] = _createWizardsPositions(players[player]);

            _createTiles();
            _setNextPlayerToPlay();
        }

        public IMovementResult Move(IWizard wizard, List<ITile> tiles)
        {
            _verifyWizardBelongsCurrentPlayer(wizard);

            foreach (ITile tile in tiles)
                tile.NatureElement.ApplyEffect(wizard);

            wizard.RemoveRemainingActions(tiles.Count);

            WizardsPositions[CurrentPlayer.Id][wizard] = tiles.Last().Position;

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

        public List<ITile> GetPreviewPositionMoves(IWizard wizard, ITile objectiveTile)
        {
            var moves = new List<ITile>();

            IPosition wizardPosition = WizardsPositions[CurrentPlayer.Id][wizard];

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
            Dictionary<IWizard, IPosition> wizardsPositions = WizardsPositions[CurrentPlayer.Id];

            KeyValuePair<IWizard, IPosition> wizardPosition =
                wizardsPositions.FirstOrDefault(p => ((Position)p.Value) == (Position)tile.Position);

            IWizard wizard = wizardPosition.Key;

            return wizard;
        }

        public void ApplyNatureElement(IWizard wizard, INatureElement natureElement, List<ITile> tiles)
        {
            _verifyWizardBelongsCurrentPlayer(wizard);

            foreach (ITile tile in tiles)
                tile.ApplyNatureElement(natureElement);

            wizard.RemoveRemainingActions(tiles.Count);

            if (!CurrentPlayer.HasRemainingActions)
                _setNextPlayerToPlay();
        }

        private void _verifyWizardBelongsCurrentPlayer(IWizard wizard)
        {
            List<IWizard> wizards = WizardsPositions[CurrentPlayer.Id].Keys.ToList();

            if (!wizards.Contains(wizard))
                throw new Exception();
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

        private void _setNextPlayerToPlay()
        {
            IPlayer nextPlayer = Players.Dequeue();
            Players.Enqueue(nextPlayer);

            CurrentPlayer = nextPlayer;
        }

        private Dictionary<IWizard, IPosition> _createWizardsPositions(List<IPosition> initialPositions)
        {
            var wizardsPosition = new Dictionary<IWizard, IPosition>();

            foreach (IPosition position in initialPositions)
            {
                var newWizard = new Wizard();

                wizardsPosition[newWizard] = position;
            }

            return wizardsPosition;
        }
    }
}
