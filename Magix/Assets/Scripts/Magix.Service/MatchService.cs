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

        public MatchService()
        {
            if (Board != null)
                throw new Exception("Already started a match.");

            var player1Wizards = new List<IWizard> {new Wizard(new Position(0, 7)), new Wizard(new Position(2, 9)),};

            var player2Wizards = new List<IWizard> {new Wizard(new Position(7, 0)), new Wizard(new Position(9, 2)),};

            var players = new List<IPlayer>
            {
                    new Player(player1Wizards),
                    new Player(player2Wizards),
            };

            Board = new Board(players);
        }
    }
}
