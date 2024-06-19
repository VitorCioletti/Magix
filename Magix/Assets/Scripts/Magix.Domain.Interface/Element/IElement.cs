namespace Magix.Domain.Interface.Element
{
    using Result;

    public interface IElement
    {
        bool Blocking { get; }

        bool CanSpread { get; }

        bool CanStack { get; }

        IEffectResult ApplyElementEffect(IWizard wizard);

        IElement GetMixedElement(IElement newElementToMix);

        bool CanReact(IElement element);
    }
}
