namespace Magix.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Player
    {
        public Guid Id { get; private set; }

        public List<Wizard> Wizards { get; private set; }

        public bool HasRemainingActions => Wizards.Any(w => w.RemainingActions > 0);

        public Player(List<Wizard> wizards)
        {
            Id = Guid.NewGuid();
            Wizards = wizards;
        }
    }
}
