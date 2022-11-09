namespace Magix.Domain.Board
{
    using System.Collections.Generic;

    public class Board
    {
        private Tile[,] _tiles;

        private const int _size = 7;

        public List<Wizard> WizardsPlayer1 { get; private set; }

        public List<Wizard> WizardsPlayer2 { get; private set; }

        public Board(List<Position> initialPositionWizardsPlayer1, List<Position> initialPositionWizardsPlayer2)
        {
            WizardsPlayer1 = new List<Wizard>();
            WizardsPlayer2 = new List<Wizard>();

            _tiles = new Tile[_size, _size];

            _createWizards(WizardsPlayer1, initialPositionWizardsPlayer1);
            _createWizards(WizardsPlayer2, initialPositionWizardsPlayer2);
        }

        private void _createWizards(List<Wizard> wizards, List<Position> initialPositions)
        {
            foreach (Position position in initialPositions)
            {
                var newWizard = new Wizard(position);

                wizards.Add(newWizard);
            }
        }
    }
}
