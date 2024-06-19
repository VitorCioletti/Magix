namespace Magix.Domain.Element
{
    using Interface;
    using Interface.Element;
    using Interface.Element.Result;

    public abstract class BaseElement : IElement
    {
        public virtual bool Blocking => false;

        public virtual bool CanSpread => false;

        public virtual bool CanStack => false;

        public abstract IEffectResult ApplyElementEffect(IWizard wizard);

        public virtual IElement GetMixedElement(IElement newElementToMix)
        {
            return new Natural();
        }

        public bool CanReact(IElement element)
        {
            return GetMixedElement(element) is not null;
        }
    }
}
