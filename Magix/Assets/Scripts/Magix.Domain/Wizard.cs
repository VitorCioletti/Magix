namespace Magix.Domain
{
    using System;
    using Interface;
    using Interface.Board;
    using Interface.Element;
    using Interface.Element.Result;
    using Element.Result;

    public class Wizard : IWizard
    {
        public Guid Id { get; private set; }

        public bool IsDead => LifePoints == 0;

        public IPosition Position { get; set; }

        public int LifePoints { get; private set; }

        public int RemainingActions { get; private set; }

        public ElementEffect ElementEffect { get; private set; }

        private const int _totalActionsPerTurn = 4;

        public Wizard(IPosition position)
        {
            Id = Guid.NewGuid();
            ElementEffect = ElementEffect.None;
            LifePoints = 5;
            RemainingActions = _totalActionsPerTurn;
            Position = position;

            ClearDebuffs();
        }

        public IEffectResult ClearDebuffs()
        {
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
            var effectResult = new EffectResult(false, true, 0);

            return effectResult;
        }

        public IEffectResult SetBlind()
        {
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

        public void ApplyElementEffect(ElementEffect elementEffect)
        {
            switch (elementEffect)
            {
                case ElementEffect.OnFire:
                    TakeDamage(1);

                    break;

                case ElementEffect.Blind:
                    RemoveRemainingActions(1);

                    break;

                case ElementEffect.Shocked:
                    RemoveRemainingActions(1);

                    break;
                case ElementEffect.Wet:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(elementEffect), elementEffect, null);
            }

            ElementEffect = elementEffect;
        }

        public int GetDistance(WizardActionType actionType)
        {

            int distance = actionType switch
            {
                WizardActionType.Move => RemainingActions,
                WizardActionType.Attack => IWizard.AttackDistance,
                WizardActionType.CastElement => IWizard.CastElementDistance,
                _ => throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null)
            };

            return distance;
        }
    }
}
