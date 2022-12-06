namespace Magix.Domain.Interface
{
    using System;
    using Board;
    using NatureElements;

    public interface IWizard
    {
        Guid Id { get; }

        int LifePoints { get; }

        IPosition Position { get; set; }

        int RemainingActions { get; }

        NatureElementEffect NatureElementEffect { get; }

        void ResetRemainingActions();

        void RemoveRemainingActions(int actions);

        bool HasRemainingActions();

        void ChangeNatureElementEffect(NatureElementEffect natureElementEffect);
    }
}
