namespace Magix.Service
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using Domain.Board;

    public class MatchService
    {
        public Board Board { get; private set; }


        public Player StartNew()
        {
            if (Board != null)
                throw new Exception("Already started a match.");

            var player1Wizards = new List<Wizard> {new(), new(),};
            var player1WizardsInitialPositions = new List<Position>
            {
                new (0, 1),
                new (0, 2),
            };

            var player2Wizards = new List<Wizard> {new(), new(),};
            var player2WizardsInitialPositions = new List<Position>
            {
                new (1, 0),
                new (2, 0),
            };

            var players = new Dictionary<Player, List<Position>>
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
