namespace Magix.Domain.Board
{
    using System;
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

        public static bool operator ==(Position position1, Position position2)
        {
            bool areXEquals = position1.X == position2.X;
            bool areYEquals = position1.Y == position2.Y;

            return areXEquals && areYEquals;
        }

        public static bool operator !=(Position position1, Position position2)
        {
            bool areXDifferent = position1.X != position2.X;
            bool areYDifferent = position1.Y != position2.Y;

            return areXDifferent || areYDifferent;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Position)
                return false;

            return Equals((Position)obj);
        }

        public override int GetHashCode() => HashCode.Combine(X, Y);

        private bool Equals(Position position)
        {
            return this != position;
        }
    }
}
