namespace Magix.Domain.Board
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interface.Board;

    public class Grid
    {
        public ITile[,] Tiles { get; private set; }

        private const int _size = 10;

        public Grid()
        {
            _createTiles();
        }

        public IList<ITile> GetPreviewArea(ITile referenceTile, int steps)
        {
            if (steps > Tiles.Length)
                throw new InvalidOperationException(
                    $"Size \"{steps}\" out of the matrix bounds \"{Tiles.Length}\".");

            IPosition referencePosition = referenceTile.Position;

            var previewArea = new List<ITile>();

            foreach (ITile tile in Tiles)
            {
                int distance = _calculateManhattanDistance(referencePosition, tile.Position);

                if (distance == 0)
                    continue;

                if (steps >= distance)
                    previewArea.Add(tile);
            }

            return previewArea;
        }

        public List<ITile> GetPreviewPathTo(ITile referenceTile, int size, ITile objectiveTile)
        {
            var moves = new List<ITile>();

            int referencePositionX = referenceTile.Position.X;
            int referencePositionY = referenceTile.Position.Y;

            int objectivePositionX = objectiveTile.Position.X;
            int objectivePositionY = objectiveTile.Position.Y;

            if (referencePositionX > objectivePositionX)
            {
                while (referencePositionX > objectivePositionX)
                {
                    referencePositionX--;

                    ITile nextTile = Tiles[referencePositionX, referencePositionY];

                    moves.Add(nextTile);
                }
            }
            else
            {
                while (referencePositionX < objectivePositionX)
                {
                    referencePositionX++;

                    ITile nextTile = Tiles[referencePositionX, referencePositionY];

                    moves.Add(nextTile);
                }
            }

            if (referencePositionY > objectivePositionY)
            {
                while (referencePositionY > objectivePositionY)
                {
                    referencePositionY--;

                    ITile nextTile = Tiles[referencePositionX, referencePositionY];

                    moves.Add(nextTile);
                }
            }
            else
            {
                while (referencePositionY < objectivePositionY)
                {
                    referencePositionY++;

                    ITile nextTile = Tiles[referencePositionX, referencePositionY];

                    moves.Add(nextTile);
                }
            }

            moves = moves.Take(size).ToList();

            return moves;
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

        private List<ITile> _getAdjacentTiles(ITile referenceTile)
        {
            var adjacentTiles = new List<ITile>();

            foreach (ITile tile in Tiles)
            {
                int distance = _calculateManhattanDistance(referenceTile.Position, tile.Position);

                if (distance == 1)
                    adjacentTiles.Add(tile);
            }

            return adjacentTiles;
        }

        private int _calculateManhattanDistance(IPosition position1, IPosition position2)
        {
            return Math.Abs(position2.X - position1.X) + Math.Abs(position2.Y - position1.Y);
        }
    }
}
