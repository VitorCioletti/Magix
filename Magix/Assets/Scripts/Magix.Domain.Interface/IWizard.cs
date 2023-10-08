namespace Magix.Domain.Interface
{
    using System;
    using Board;
    using NatureElements;
    using NatureElements.Result;

    public interface IWizard
    {
        Guid Id { get; }

        int LifePoints { get; }

        bool CanAttack { get; }

        bool CanMove { get; }

        IPosition Position { get; set; }

        int RemainingActions { get; }

        NatureElementEffect NatureElementEffect { get; }

        IEffectResult ClearDebuffs();

        IEffectResult Stun();

        IEffectResult SetBlind();

        IEffectResult TakeDamage(int damage);

        void ResetRemainingActions();

        void RemoveRemainingActions(int actions);

        bool HasRemainingActions();

        void ChangeNatureElementEffect(NatureElementEffect natureElementEffect);
    }
}
