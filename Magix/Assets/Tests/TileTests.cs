﻿namespace Tests
{
    using System.Collections.Generic;
    using Magix.Domain.Board;
    using Magix.Domain.Interface.Board;
    using Magix.Domain.Interface.Element;
    using Magix.Domain.Element;
    using NSubstitute;
    using NUnit.Framework;

    public class TileTests
    {
        [Test]
        public void HasElementMustReturnTrue()
        {
            var position = new Position(0, 0);
            var tile = new Tile(position);
            var element = Substitute.For<IElement>();

            tile.Mix(element);

            Assert.IsTrue(tile.Element.Contains(element));
        }

        [Test]
        public void HasElementMustReturnFalse()
        {
            var position = new Position(0, 0);
            var tile = new Tile(position);

            Assert.IsFalse(tile.HasElement<Water>());
        }

        [Test]
        public void CanReactOnSelfMustReturnTrue()
        {
            var position = new Position(0, 0);
            var tile = new Tile(position);

            var originalElementOnTile = Substitute.For<IElement>();
            var elementToMix = Substitute.For<IElement>();

            originalElementOnTile.CanReact(elementToMix).Returns(true);

            tile.Mix(originalElementOnTile);

            Assert.IsTrue(tile.CanReactOnSelf(elementToMix));
        }

        [Test]
        public void CanReactOnSelfMustReturnFalse()
        {
            var position = new Position(0, 0);
            var tile = new Tile(position);
            var element = Substitute.For<IElement>();

            tile.Mix(element);

            Assert.IsFalse(tile.CanReactOnSelf(element));
        }

        [Test]
        public void MustSpreadElementOnAdjacentTilesIfCanReact()
        {
            var position = new Position(0, 0);

            var elementToSpread = Substitute.For<IElement>();
            elementToSpread.CanSpread.Returns(true);

            var mixableOriginalElement = Substitute.For<IElement>();

            mixableOriginalElement.CanStack.Returns(true);
            mixableOriginalElement.CanReact(elementToSpread).Returns(true);
            mixableOriginalElement.GetMixedElement(elementToSpread).Returns(elementToSpread);

            var unMixableElement = Substitute.For<IElement>();

            mixableOriginalElement.CanStack.Returns(false);
            mixableOriginalElement.CanReact(elementToSpread).Returns(false);

            var tile = new Tile(position);

            var tile1 = new Tile(position);
            var tile2 = new Tile(position);
            var tile3 = new Tile(position);
            var tile4 = new Tile(position);

            tile1.Mix(mixableOriginalElement);
            tile2.Mix(mixableOriginalElement);
            tile3.Mix(mixableOriginalElement);

            tile4.Mix(unMixableElement);

            var adjacentTiles = new List<ITile> {tile1, tile2, tile3, tile4};

            tile.SetAdjacent(adjacentTiles);

            tile.Mix(elementToSpread);

            Assert.IsTrue(tile1.Element.Contains(elementToSpread));
            Assert.IsTrue(tile2.Element.Contains(elementToSpread));
            Assert.IsTrue(tile3.Element.Contains(elementToSpread));

            Assert.IsFalse(tile4.Element.Contains(elementToSpread));
        }

        [Test]
        public void MustNotSpreadElementOnAdjacentTiles()
        {
            var position = new Position(0, 0);

            var newElementToMix = Substitute.For<IElement>();
            newElementToMix.CanSpread.Returns(false);

            var mixableOriginalElement = Substitute.For<IElement>();

            mixableOriginalElement.CanReact(newElementToMix).Returns(true);
            mixableOriginalElement.GetMixedElement(newElementToMix).Returns(newElementToMix);

            var unMixableElement = Substitute.For<IElement>();

            mixableOriginalElement.CanStack.Returns(false);
            mixableOriginalElement.CanReact(newElementToMix).Returns(false);

            var tile = new Tile(position);

            var tile1 = new Tile(position);
            var tile2 = new Tile(position);
            var tile3 = new Tile(position);
            var tile4 = new Tile(position);

            tile1.Mix(mixableOriginalElement);
            tile2.Mix(mixableOriginalElement);
            tile3.Mix(mixableOriginalElement);

            tile4.Mix(unMixableElement);

            var adjacentTiles = new List<ITile> {tile1, tile2, tile3, tile4};

            tile.SetAdjacent(adjacentTiles);

            tile.Mix(newElementToMix);

            Assert.IsTrue(tile.Element.Contains(newElementToMix));

            Assert.IsFalse(tile1.Element.Contains(newElementToMix));
            Assert.IsFalse(tile2.Element.Contains(newElementToMix));
            Assert.IsFalse(tile3.Element.Contains(newElementToMix));

            Assert.IsFalse(tile4.Element.Contains(newElementToMix));
        }

        [Test]
        public void MustStackElement()
        {
            var position = new Position(0, 0);

            var newElementToMix = Substitute.For<IElement>();

            newElementToMix.CanStack.Returns(true);
            newElementToMix.CanSpread.Returns(false);

            var originalElement = Substitute.For<IElement>();

            originalElement.CanReact(newElementToMix).Returns(true);
            originalElement.GetMixedElement(newElementToMix).Returns(newElementToMix);
            originalElement.CanStack.Returns(false);
            originalElement.CanReact(newElementToMix).Returns(false);

            var tile = new Tile(position);

            tile.Mix(originalElement);
            tile.Mix(newElementToMix);

            Assert.IsTrue(tile.Element.Contains(originalElement));
            Assert.IsTrue(tile.Element.Contains(newElementToMix));
        }

        [Test]
        public void MustNotStackElement()
        {
            var position = new Position(0, 0);

            var newElementToMix = Substitute.For<IElement>();

            newElementToMix.CanStack.Returns(false);
            newElementToMix.CanSpread.Returns(false);

            var originalElement = Substitute.For<IElement>();

            originalElement.CanReact(newElementToMix).Returns(true);
            originalElement.GetMixedElement(newElementToMix).Returns(newElementToMix);
            originalElement.CanStack.Returns(false);
            originalElement.CanReact(newElementToMix).Returns(false);

            var tile = new Tile(position);

            tile.Mix(originalElement);
            tile.Mix(newElementToMix);

            Assert.IsFalse(tile.Element.Contains(originalElement));
            Assert.IsTrue(tile.Element.Contains(newElementToMix));
        }
    }
}
