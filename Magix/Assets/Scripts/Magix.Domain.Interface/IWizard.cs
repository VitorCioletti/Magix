namespace Magix.Domain.Interface
{
    using System;
    using NatureElements;

    public interface IWizard
    {
        Guid Id { get; }

        int LifePoints { get; }

        int RemainingActions { get; }

        NatureElementEffect NatureElementEffect { get; }

        void RemoveRemainingActions(int actionsRemoved);

        void ChangeNatureElementEffect(NatureElementEffect natureElementEffect);
    }
}
