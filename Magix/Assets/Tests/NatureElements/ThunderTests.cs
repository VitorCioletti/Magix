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
        public void ThunderMustStackWithWater()
        {
            var thunder = new Thunder();
            var water = new Water();

            var tile = new Tile(new Position());

            tile.Mix(water);
            tile.Mix(thunder);

            Assert.IsTrue(tile.Elements.Contains(thunder));
            Assert.IsTrue(tile.Elements.Contains(water));
        }

        [Test]
        public void ThunderMustSpreadInAdjacentWater()
        {
            var thunder = new Thunder();
            var water = new Water();

            var tile = new Tile(new Position(0, 1));
            var adjacentTile1 = new Tile(new Position(0, 2));
            var adjacentTile2 = new Tile(new Position(0, 3));
            var adjacentTile3 = new Tile(new Position(0, 4));
            var adjacentTile4 = new Tile(new Position(0, 5));

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

            var mixResultAdjacent1TileWithWater = new MixResult(
                adjacentTile1,
                thunder,
                water,
                thunder);

            var mixResultAdjacent2TileWithWater = new MixResult(
                adjacentTile2,
                thunder,
                water,
                thunder);

            var mixResultAdjacent3TileWithWater = new MixResult(
                adjacentTile3,
                thunder,
                water,
                thunder);

            var mixResultAdjacentTileWithoutWater = new MixResult(
                adjacentTile4,
                thunder,
                adjacentTile4.Elements[0],
                null,
                false,
                "cant-mix");

            var expectedMixResults = new List<IMixResult>
            {
                mixResultTile,
                mixResultAdjacent1TileWithWater,
                mixResultAdjacent2TileWithWater,
                mixResultAdjacent3TileWithWater,
                mixResultAdjacentTileWithoutWater
            };

            List<IMixResult> mixResults = tile.Mix(thunder);

            for (int i = 0; i < expectedMixResults.Count; i++)
            {
                Assert.AreEqual(expectedMixResults[i].Success, mixResults[i].Success);
                Assert.AreEqual(expectedMixResults[i].AffectedTile, mixResults[i].AffectedTile);
                Assert.AreEqual(expectedMixResults[i].NewElement, mixResults[i].NewElement);
                Assert.AreEqual(expectedMixResults[i].OriginallyOnTile, mixResults[i].OriginallyOnTile);
                Assert.AreEqual(expectedMixResults[i].TriedToMix, mixResults[i].TriedToMix);
            }
        }
    }
}
