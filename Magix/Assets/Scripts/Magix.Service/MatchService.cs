namespace Magix.Service
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using Domain.Board;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Interface;

    public class MatchService : IMatchService
    {
        public IBoard Board { get; private set; }

        public IPlayer StartNew()
        {
            if (Board != null)
                throw new Exception("Already started a match.");

            var player1Wizards = new List<IWizard> {new Wizard(), new Wizard(),};
            var player1WizardsInitialPositions = new List<IPosition>
            {
                new Position(0, 1),
                new Position(0, 2),
            };

            var player2Wizards = new List<IWizard> {new Wizard(), new Wizard(),};
            var player2WizardsInitialPositions = new List<IPosition>
            {
                new Position(1, 0),
                new Position(2, 0),
            };

            var players = new Dictionary<IPlayer, List<IPosition>>
            {
                {
                    new Player(player1Wizards),
                    player1WizardsInitialPositions
                },
                {
                    new Player(player2Wizards),
                    player2WizardsInitialPositions
                }
            };

            Board = new Board(players);

            return Board.CurrentPlayer;
        }
    }
}
