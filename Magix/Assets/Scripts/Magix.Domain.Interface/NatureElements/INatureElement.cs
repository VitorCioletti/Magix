namespace Magix.Domain.Interface.NatureElements
{
    using Result;

    public interface INatureElement
    {
        bool Blocking { get; }

        bool CanSpread { get; }

        IEffectResult ApplyElementEffect(IWizard wizard);

        INatureElement GetMixedElement(INatureElement natureElement);

        bool CanReact(INatureElement natureElement);
    }
}
