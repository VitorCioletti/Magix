namespace Tests
{
    using System.Collections.Generic;
    using Magix.Domain;
    using Magix.Domain.Board;
    using Magix.Domain.Board.Result;
    using Magix.Domain.Interface;
    using Magix.Domain.Interface.Board;
    using Magix.Domain.Interface.Board.Result;
    using Magix.Domain.Interface.NatureElements;
    using Magix.Domain.NatureElements;
    using NSubstitute;
    using NUnit.Framework;

    public class BoardTests
    {
        private Board _board;

        private List<IPlayer> _players;

        [SetUp]
        public void Configure()
        {
            var player1Wizards = new List<IWizard> {new Wizard(new Position(0, 7)), new Wizard(new Position(2, 9))};
            var player2Wizards = new List<IWizard> {new Wizard(new Position(7, 0)), new Wizard(new Position(9, 2))};

            _players = new List<IPlayer> {new Player(player1Wizards, 1), new Player(player2Wizards, 2)};

            _board = new Board(_players);
        }

        [Test]
        public void WizardCanCastFireOverNatural()
        {
            IPlayer currentPlayer = _board.CurrentPlayer;
            IWizard wizard = currentPlayer.Wizards[0];

            IPosition wizardPosition = wizard.Position;

            ITile tileToCast = _board.Tiles[wizardPosition.X + 1, wizardPosition.Y];

            var fire = Substitute.For<IFire>();

            var expectedResultedMix = Substitute.For<IMixResult>();

            expectedResultedMix.AffectedTile.Returns(tileToCast);
            expectedResultedMix.TriedToMix.Returns(fire);
            expectedResultedMix.OriginallyOnTile.Returns(tileToCast.NatureElements[0]);
            expectedResultedMix.NewElement.Returns(fire);

            const int expectedMixes = 1;

            var tilesToCast = new List<ITile> {tileToCast};

            ICastResult castResult = _board.CastNatureElement(wizard, fire, tilesToCast);

            Assert.IsTrue(castResult.Success);

            Assert.AreEqual(expectedMixes, castResult.ResultedMixes.Count);

            IMixResult resultedMix = castResult.ResultedMixes[0];

            Assert.AreEqual(expectedResultedMix.AffectedTile, resultedMix.AffectedTile);
            Assert.AreEqual(expectedResultedMix.TriedToMix, resultedMix.TriedToMix);
            Assert.AreEqual(expectedResultedMix.NewElement, resultedMix.NewElement);
            Assert.AreEqual(expectedResultedMix.OriginallyOnTile, resultedMix.OriginallyOnTile);

            Assert.AreEqual(fire, tileToCast.NatureElements[0]);
        }

        [Test]
        public void WizardCantCastFireOverFire()
        {
            IPlayer currentPlayer = _board.CurrentPlayer;
            IWizard wizard = currentPlayer.Wizards[0];

            IPosition wizardPosition = wizard.Position;

            ITile tileToCast = _board.Tiles[wizardPosition.X + 1, wizardPosition.Y];

            var fire = new Fire();

            var expectedResultedMix = new MixResult(
                tileToCast,
                fire,
                fire,
                null,
                false,
                "cant-mix");

            var tilesToCast = new List<ITile> {tileToCast};

            _board.CastNatureElement(wizard, fire, tilesToCast);

            ICastResult castResult = _board.CastNatureElement(wizard, fire, tilesToCast);

            Assert.IsFalse(castResult.Success);

            IMixResult resultedMix = castResult.ResultedMixes[0];

            Assert.AreEqual(expectedResultedMix.AffectedTile, resultedMix.AffectedTile);
            Assert.AreEqual(expectedResultedMix.TriedToMix, resultedMix.TriedToMix);
            Assert.AreEqual(expectedResultedMix.NewElement, resultedMix.NewElement);
            Assert.AreEqual(expectedResultedMix.OriginallyOnTile, resultedMix.OriginallyOnTile);
        }

        [Test]
        public void WizardCantWalkOverBlockingElement()
        {
            IPlayer currentPlayer = _board.CurrentPlayer;
            IWizard wizard = currentPlayer.Wizards[0];

            IPosition wizardPosition = wizard.Position;

            var elementPosition = new Position(wizardPosition.X + 1, wizardPosition.Y);

            ITile firstTile = _board.Tiles[elementPosition.X, elementPosition.Y];
            ITile nextTile = _board.Tiles[elementPosition.X + 1, elementPosition.Y];

            var blockingElement = Substitute.For<INatureElement>();
            blockingElement.Blocking.Returns(true);

            var tilesToCast = new List<ITile> {firstTile};
            var tilesToMove = new List<ITile> {firstTile, nextTile};

            _board.CastNatureElement(wizard, blockingElement, tilesToCast);

            var expectedMovementResult = Substitute.For<IMovementResult>();

            expectedMovementResult.Steps.Returns(new List<IStepResult>());
            expectedMovementResult.Success.Returns(false);
            expectedMovementResult.ErrorId.Returns(MovementResult.CantGoThroughBlockingElement);

            IMovementResult movementResult = _board.Move(wizard, tilesToMove);

            Assert.AreEqual(expectedMovementResult.Steps, movementResult.Steps);
            Assert.AreEqual(expectedMovementResult.Success, movementResult.Success);
            Assert.AreEqual(expectedMovementResult.ErrorId, movementResult.ErrorId);
        }

        [Test]
        public void WizardCanWalkOverNonBlockingElement()
        {
            IPlayer currentPlayer = _board.CurrentPlayer;
            IWizard wizard = currentPlayer.Wizards[0];

            IPosition wizardPosition = wizard.Position;

            var elementPosition = new Position(wizardPosition.X + 1, wizardPosition.Y);

            ITile firstTile = _board.Tiles[elementPosition.X, elementPosition.Y];
            ITile secondTile = _board.Tiles[elementPosition.X + 1, elementPosition.Y];

            var nonBlockingElement = Substitute.For<INatureElement>();

            var tilesToCast = new List<ITile> {firstTile};
            var tilesToMove = new List<ITile> {firstTile, secondTile};

            _board.CastNatureElement(wizard, nonBlockingElement, tilesToCast);

            var expectedMovementResult = Substitute.For<IMovementResult>();

            expectedMovementResult.Success.Returns(true);
            expectedMovementResult.ErrorId.Returns(string.Empty);

            IMovementResult movementResult = _board.Move(wizard, tilesToMove);

            Assert.AreEqual(expectedMovementResult.Success, movementResult.Success);
            Assert.AreEqual(expectedMovementResult.ErrorId, movementResult.ErrorId);
        }
    }
}
