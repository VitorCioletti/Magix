namespace Tests
{
    using System.Collections.Generic;
    using Magix.Domain.Board;
    using Magix.Domain.Interface.Board;
    using NUnit.Framework;

    public class GridTests
    {
        /*

         Given a 10x10 matrix, a reference position in [0, 0] and the size of 3, the function must return the
         following positions:

           0 1 2 3 4 5 6 7 8 9
         0   x x x
         1 x x x
         2 x x
         3 x
         4
         5
         6
         7
         8
         9
        */

        [Test]
        public void MustGetGridArea()
        {
            var position = new Position(0, 0);
            var referenceTile = new Tile(position);
            int size = 3;

            var expectedPositions = new List<IPosition>
            {
                new Position(0, 1),
                new Position(0, 2),
                new Position(0, 3),
                new Position(1, 0),
                new Position(1, 1),
                new Position(1, 2),
                new Position(2, 0),
                new Position(2, 1),
                new Position(3, 0),
            };

            var grid = new Grid();

            IList<ITile> previewArea = grid.GetPreviewArea(referenceTile, size);

            Assert.AreEqual(expectedPositions.Count, previewArea.Count);

            for (var i = 0; i < expectedPositions.Count; i++)
            {
                Assert.AreEqual(expectedPositions[i], previewArea[i].Position);
            }
        }
    }
}
