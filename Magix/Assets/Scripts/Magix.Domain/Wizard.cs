namespace Magix.Domain
{
    using System;
    using Interface;
    using Interface.Board;
    using Interface.NatureElements;
    using Interface.NatureElements.Result;
    using NatureElements.Result;

    public class Wizard : IWizard
    {
        public Guid Id { get; private set; }

        public IPosition Position { get; set; }

        public bool CanAttack { get; private set; }

        public bool CanPush { get; private set; }

        public bool CanMove { get; private set; }

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

            ClearDebuffs();
        }

        public IEffectResult ClearDebuffs()
        {
            CanAttack = true;
            CanMove = true;
            CanPush = true;

            var effectResult = new EffectResult(false, false, 0);

            return effectResult;

        }

        public IEffectResult TakeDamage(int damage)
        {
            LifePoints -= damage;

            var effectResult = new EffectResult(false, false, damage);

            return effectResult;
        }

        public IEffectResult Stun()
        {
            CanAttack = false;
            CanMove = false;
            CanPush = false;

            var effectResult = new EffectResult(false, true, 0);

            return effectResult;
        }

        public IEffectResult SetBlind()
        {
            CanAttack = false;
            CanPush = false;

            var effectResult = new EffectResult(true, false, 0);

            return effectResult;
        }

        public void ResetRemainingActions()
        {
            RemainingActions = _totalActionsPerTurn;
        }

        public void RemoveRemainingActions(int actions)
        {
            RemainingActions -= actions;
        }

        public bool HasRemainingActions() => RemainingActions > 0;

        public void ChangeNatureElementEffect(NatureElementEffect natureElementEffect)
        {
            switch (natureElementEffect)
            {
                case NatureElementEffect.OnFire:
                    TakeDamage(1);

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

        public int GetDistance(WizardActionType actionType)
        {
            int distance = 0;

            switch (actionType)
            {

                case WizardActionType.Move:
                    distance = RemainingActions;

                    break;
                case WizardActionType.Attack:
                    distance = IWizard.AttackDistance;

                    break;
                case WizardActionType.CastNatureElement:
                {
                    if (RemainingActions > IWizard.CastNatureElementDistance)
                        distance = IWizard.CastNatureElementDistance;
                    else
                        distance = RemainingActions - IWizard.CastNatureElementDistance;

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null);
            }

            return distance;
        }
    }
}
