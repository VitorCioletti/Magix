namespace Magix.Domain.Interface
{
    using System;
    using Board;
    using Element;
    using Element.Result;

    public interface IWizard
    {
        static int AttackDamage => 1;
        static int AttackDistance => 1;
        static int CastElementDistance => 5;

        Guid Id { get; }

        int LifePoints { get; }

        IPosition Position { get; set; }

        int RemainingActions { get; }

        ElementEffect ElementEffect { get; }

        IEffectResult ClearDebuffs();

        IEffectResult Stun();

        IEffectResult SetBlind();

        IEffectResult TakeDamage(int damage);

        void ResetRemainingActions();

        void RemoveRemainingActions(int actions);

        bool HasRemainingActions();

        void ApplyElementEffect(ElementEffect elementEffect);

        int GetDistance(WizardActionType actionType);
    }
}
