namespace Magix.Domain
{
    using System;
    using Interface;
    using Interface.NatureElements;

    public class Wizard : IWizard
    {
        public Guid Id { get; private set; }

        public int LifePoints { get; private set; }

        public int RemainingActions { get; private set; }

        public NatureElementEffect NatureElementEffect { get; private set; }

        public Wizard()
        {
            Id = Guid.NewGuid();
            NatureElementEffect = NatureElementEffect.None;
            LifePoints = 5;
            RemainingActions = 4;
        }

        public void RemoveRemainingActions(int actionsRemoved)
        {
            RemainingActions -= actionsRemoved;
        }

        public void ChangeNatureElementEffect(NatureElementEffect natureElementEffect)
        {
            switch (natureElementEffect)
            {
                case NatureElementEffect.OnFire:
                    _takeDamage(1);
                    break;

                case NatureElementEffect.Blind:
                    RemoveRemainingActions(1);
                    break;

                case NatureElementEffect.Shocked:
                    RemoveRemainingActions(1);
                    break;
            }

            NatureElementEffect = natureElementEffect;
        }

        private void _takeDamage(int damage)
        {
            LifePoints -= damage;
        }
    }
}
