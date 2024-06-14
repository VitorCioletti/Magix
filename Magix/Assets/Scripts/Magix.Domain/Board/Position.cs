namespace Magix.Domain.Board
{
    using System;
    using Interface.Board;

    public readonly struct Position : IPosition
    {
        public override int GetHashCode() => HashCode.Combine(X, Y);

        public int X { get; }

        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Position position, Position other)
        {
            return position.Equals(other);
        }

        public static bool operator !=(Position position, Position other)
        {
            return !position.Equals(other);
        }

        public override bool Equals(object obj)
        {
            return obj is Position other && Equals(other);
        }

        public bool Equals(IPosition position)
        {
            bool areXEquals = X == position.X;
            bool areYEquals = Y == position.Y;

            return areXEquals && areYEquals;
        }
    }
}
