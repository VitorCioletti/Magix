namespace Magix.Domain.NatureElements
{
    public abstract class BaseNatureElement
    {
        public abstract NatureElementEffect Effect { get; }

        public void ApplyEffect(Wizard wizard) => wizard.ChangeNatureElementEffect(Effect);

        public abstract BaseNatureElement ApplyNatureElement(BaseNatureElement natureElement);
    }
}
