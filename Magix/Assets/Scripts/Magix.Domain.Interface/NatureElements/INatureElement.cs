namespace Magix.Domain.Interface.NatureElements
{
    public interface INatureElement
    {
        NatureElementEffect Effect { get; }

        void ApplyEffect(IWizard wizard);

        INatureElement ApplyNatureElement(INatureElement natureElement);
    }
}
