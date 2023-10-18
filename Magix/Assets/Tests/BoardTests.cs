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
                tileToCast.Elements[0],
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

            Assert.AreEqual(fire, tileToCast.Elements[0]);
        }
    }
}
