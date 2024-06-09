namespace Tests
{
    using Magix.Domain;
    using Magix.Domain.Board;
    using Magix.Domain.Interface;
    using NUnit.Framework;
    using System.Collections.Generic;
    using Magix.Domain.Board.Result;
    using Magix.Domain.Interface.Board;
    using Magix.Domain.Interface.Board.Result;
    using Magix.Domain.NatureElements;
    using Magix.Domain.NatureElements.Result;

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

            var fire = new Fire();

            var expectedResultedMix = new MixResult(
                tileToCast,
                fire,
                tileToCast.NatureElements[0],
                fire);

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
        public void WizardCanWalkOverFire()
        {
            IPlayer currentPlayer = _board.CurrentPlayer;
            IWizard wizard = currentPlayer.Wizards[0];

            IPosition wizardPosition = wizard.Position;

            var firePosition = new Position(wizardPosition.X + 1, wizardPosition.Y);

            ITile firstTile = _board.Tiles[firePosition.X, firePosition.Y];
            ITile nextTile = _board.Tiles[firePosition.X + 1, firePosition.Y];

            var fire = new Fire();

            var tilesToCast = new List<ITile> {firstTile};
            var tilesToMove = new List<ITile> {firstTile, nextTile};

            var firstStepEffect = new EffectResult(
                false,
                true,
                false,
                Fire.Damage);

            var firstStep = new StepResult(firstTile, firstStepEffect);

            var secondStepEffect = new EffectResult(
                false,
                false,
                false,
                0);

            var secondStep = new StepResult(nextTile, secondStepEffect);

            var allSteps = new List<IStepResult> {firstStep, secondStep};

            var expectedMovementResult = new MovementResult(allSteps, true, string.Empty);

            _board.CastNatureElement(wizard, fire, tilesToCast);

            var movementResult = _board.Move(wizard, tilesToMove);

            Assert.AreEqual(expectedMovementResult.Steps[0].Effect, movementResult.Steps[0].Effect);
            Assert.AreEqual(expectedMovementResult.Steps[0].Tile, movementResult.Steps[0].Tile);

            Assert.AreEqual(expectedMovementResult.Steps[1].Effect, movementResult.Steps[1].Effect);
            Assert.AreEqual(expectedMovementResult.Steps[1].Tile, movementResult.Steps[1].Tile);
        }
    }
}
