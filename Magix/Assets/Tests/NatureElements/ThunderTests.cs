namespace Tests.NatureElements
{
    using System.Collections.Generic;
    using Magix.Domain.Board;
    using Magix.Domain.Board.Result;
    using Magix.Domain.Interface.Board;
    using Magix.Domain.Interface.Board.Result;
    using Magix.Domain.Interface.NatureElements;
    using Magix.Domain.NatureElements;
    using NUnit.Framework;

    public class ThunderTests
    {
        [Test]
        public void ThunderCantMixWithNatural()
        {
            var thunder = new Thunder();
            var natural = new Natural();

            var mixedElement = natural.GetMixedElement(thunder);

            Assert.AreEqual(null, mixedElement);
        }

        [Test]
        public void ThunderCanMixWithWater()
        {
            var thunder = new Thunder();
            var water = new Water();

            INatureElement mixedElement = water.GetMixedElement(thunder);

            Assert.AreEqual(thunder, mixedElement);
        }

        [Test]
        public void ThunderMustSpreadInAdjacentWater()
        {
            var thunder = new Thunder();
            var water = new Water();

            var tile = new Tile(new Position());
            var adjacentTile1 = new Tile(new Position());
            var adjacentTile2 = new Tile(new Position());
            var adjacentTile3 = new Tile(new Position());
            var adjacentTile4 = new Tile(new Position());

            tile.Mix(water);

            adjacentTile1.Mix(water);
            adjacentTile2.Mix(water);
            adjacentTile3.Mix(water);

            var adjacent = new List<ITile> {adjacentTile1, adjacentTile2, adjacentTile3, adjacentTile4};

            tile.SetAdjacent(adjacent);

            var mixResultTile = new MixResult(
                tile,
                thunder,
                water,
                thunder);

            var mixResultAdjacentTileWithWater = new MixResult(
                tile,
                thunder,
                water,
                thunder);

            var mixResultAdjacentTileWithoutWater = new MixResult(
                tile,
                thunder,
                adjacentTile4.Elements[0],
                adjacentTile4.Elements[0],
                false);

            var expectedMixResults = new List<IMixResult>
            {
                mixResultTile,
                mixResultAdjacentTileWithWater,
                mixResultAdjacentTileWithWater,
                mixResultAdjacentTileWithWater,
                mixResultAdjacentTileWithoutWater
            };

            tile.Mix(water);

            List<IMixResult> mixResults = tile.Mix(thunder);

            for (int i = 0; i < mixResults.Count; i++)
            {
                Assert.AreEqual(expectedMixResults[i].AffectedTile, mixResults[i].AffectedTile);
                Assert.AreEqual(expectedMixResults[i].NewElement, mixResults[i].NewElement);
                Assert.AreEqual(expectedMixResults[i].OriginallyOnTile, mixResults[i].OriginallyOnTile);
                Assert.AreEqual(expectedMixResults[i].TriedToMix, mixResults[i].TriedToMix);
            }
        }
    }
}
