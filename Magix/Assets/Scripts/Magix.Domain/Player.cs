namespace Magix.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interface;

    public class Player : IPlayer
    {
        public Guid Id { get; private set; }

        public List<IWizard> Wizards { get; private set; }

        public bool HasRemainingActions => Wizards.Any(w => w.RemainingActions > 0);

        public Player(List<IWizard> wizards)
        {
            Id = Guid.NewGuid();
            Wizards = wizards;
        }
    }
}
