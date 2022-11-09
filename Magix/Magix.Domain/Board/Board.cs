namespace Magix.Domain.Board
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NatureElements;

    public class Board
    {
        public Tile[,] Tiles { get; private set; }

        public Queue<Player> Players { get; private set; }

        public Player CurrentPlayer { get; private set; }

        private readonly Dictionary<Guid, Dictionary<Wizard, Position>> _wizardsPosition;

        private const int _size = 7;

        public Board(Dictionary<Player, List<Position>> players)
        {
            _wizardsPosition = new Dictionary<Guid, Dictionary<Wizard, Position>>();
            Players = new Queue<Player>(players.Keys.ToList());

            foreach (Player player in Players)
                _wizardsPosition[player.Id] = _createWizardsPositions(players[player]);

            Tiles = new Tile[_size, _size];

             _setNextPlayerToPlay();
        }

        public void Move(Wizard wizard, List<Tile> tiles)
        {
            _verifyWizardBelongsCurrentPlayer(wizard);

            foreach (Tile tile in tiles)
                tile.NatureElement.ApplyEffect(wizard);

            wizard.RemoveRemainingActions(tiles.Count);

            _wizardsPosition[CurrentPlayer.Id][wizard] = tiles.Last().Position;
        }

        public void ApplyNatureElement(Wizard wizard, BaseNatureElement natureElement, List<Tile> tiles)
        {
            _verifyWizardBelongsCurrentPlayer(wizard);

            foreach (Tile tile in tiles)
                tile.ApplyNatureElement(natureElement);

            wizard.RemoveRemainingActions(tiles.Count);

            if (!CurrentPlayer.HasRemainingActions)
                _setNextPlayerToPlay();
        }

        private void _verifyWizardBelongsCurrentPlayer(Wizard wizard)
        {
            List<Wizard> wizards = _wizardsPosition[CurrentPlayer.Id].Keys.ToList();

            if (!wizards.Contains(wizard))
                throw new Exception();
        }

        private void _setNextPlayerToPlay()
        {
            Player nextPlayer = Players.Dequeue();
            Players.Enqueue(nextPlayer);

            CurrentPlayer = nextPlayer;
        }

        private Dictionary<Wizard, Position> _createWizardsPositions(List<Position> initialPositions)
        {
            var wizardsPosition = new Dictionary<Wizard, Position>();

            foreach (Position position in initialPositions)
            {
                var newWizard = new Wizard();

                wizardsPosition[newWizard] = position;
            }

            return wizardsPosition;
        }
    }
}
