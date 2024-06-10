namespace Magix.Domain.Interface.NatureElements
{
    using Result;

    public interface INatureElement
    {
        bool Blocking { get; }

        bool CanSpread { get; }

        bool CanStack { get; }

        IEffectResult ApplyElementEffect(IWizard wizard);

        INatureElement GetMixedElement(INatureElement newElementToMix);

        bool CanReact(INatureElement natureElement);
    }
}
