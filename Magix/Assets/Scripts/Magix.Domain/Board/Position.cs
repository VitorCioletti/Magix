namespace Magix.Domain.Board
{
    using Interface.Board;

    public readonly struct Position : IPosition
    {
        public int X { get; }

        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(IPosition position)
        {
            bool areXEquals = X == position.X;
            bool areYEquals = Y == position.Y;

            return areXEquals && areYEquals;
        }
    }
}
