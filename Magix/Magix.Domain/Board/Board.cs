namespace Magix.Domain.Board
{
    public class Board
    {
        private Tile[,] _tiles;

        private const int _size = 7;

        public Board()
        {
            _tiles = new Tile[_size, _size];
        }
    }
}
