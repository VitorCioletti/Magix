namespace Magix.Domain.NatureElements
{
    using Interface;
    using Interface.NatureElements;

    public abstract class BaseNatureElement : INatureElement
    {
        public virtual bool Blocking => false;

        public virtual bool CanSpread => false;

        public abstract NatureElementEffect Effect { get; }

        public void ApplyElementEffect(IWizard wizard) => wizard.ChangeNatureElementEffect(Effect);

        public abstract INatureElement GetMixedElement(INatureElement natureElement);

        public bool CanReact(INatureElement natureElement)
        {
            return GetMixedElement(natureElement) is not null;
        }
    }
}
