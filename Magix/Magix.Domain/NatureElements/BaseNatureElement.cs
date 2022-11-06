namespace Magix.Domain.NatureElements
{
    public abstract class BaseNatureElement
    {
        protected abstract NatureElementEffect Effect { get; }

        public void ApplyEffect(Wizard wizard) => wizard.ChangeNatureElementEffect(Effect);

        public abstract BaseNatureElement ApplyNatureElement(BaseNatureElement natureElement);
    }
}
