namespace Magix.Domain.NatureElements
{
    using Interface;
    using Interface.NatureElements;

    public abstract class BaseNatureElement : INatureElement
    {
        public abstract NatureElementEffect Effect { get; }

        public void ApplyEffect(IWizard wizard) => wizard.ChangeNatureElementEffect(Effect);

        public abstract INatureElement CastNatureElement(INatureElement natureElement);
    }
}
