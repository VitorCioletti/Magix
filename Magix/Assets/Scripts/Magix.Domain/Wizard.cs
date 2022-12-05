namespace Magix.Domain
{
    using System;
    using Interface;
    using Interface.Board;
    using Interface.NatureElements;

    public class Wizard : IWizard
    {
        public Guid Id { get; private set; }

        public IPosition Position { get; set; }

        public int LifePoints { get; private set; }

        public int RemainingActions { get; private set; }

        public NatureElementEffect NatureElementEffect { get; private set; }

        private const int _totalActionsPerTurn = 4;

        public Wizard(IPosition position)
        {
            Id = Guid.NewGuid();
            NatureElementEffect = NatureElementEffect.None;
            LifePoints = 5;
            RemainingActions = _totalActionsPerTurn;
            Position = position;
        }

        public void ResetRemainingActions()
        {
            RemainingActions = _totalActionsPerTurn;
        }

        public void RemoveRemainingActions(int actions)
        {
            RemainingActions -= actions;
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
