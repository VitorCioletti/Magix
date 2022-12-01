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

        private readonly Dictionary<Guid, Dictionary<IWizard, IPosition>> _wizardsPosition;

        private const int _size = 7;

        public Board(Dictionary<IPlayer, List<IPosition>> players)
        {
            _wizardsPosition = new Dictionary<Guid, Dictionary<IWizard, IPosition>>();
            Players = new Queue<IPlayer>(players.Keys.ToList());

            foreach (IPlayer player in Players)
                _wizardsPosition[player.Id] = _createWizardsPositions(players[player]);

            Tiles = new ITile[_size, _size];

            _setNextPlayerToPlay();
        }

        public IMovementResult Move(IWizard wizard, List<ITile> tiles)
        {
            _verifyWizardBelongsCurrentPlayer(wizard);

            foreach (ITile tile in tiles)
                tile.NatureElement.ApplyEffect(wizard);

            wizard.RemoveRemainingActions(tiles.Count);

            _wizardsPosition[CurrentPlayer.Id][wizard] = tiles.Last().Position;

            return new MovementResult(
                tiles,
                wizard.Id.ToString(),
                true,
                string.Empty);
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
            List<IWizard> wizards = _wizardsPosition[CurrentPlayer.Id].Keys.ToList();

            if (!wizards.Contains(wizard))
                throw new Exception();
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
