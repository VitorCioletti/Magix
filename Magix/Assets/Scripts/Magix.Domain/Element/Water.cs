namespace Magix.Domain.Element
{
    using Interface;
    using Interface.Element;
    using Interface.Element.Result;

    public class Water : BaseElement, IWater
    {
        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return null;
        }

        public override IElement GetMixedElement(IElement newElementToMix)
        {
            IElement resultantElement = newElementToMix switch
            {
                Fire => new Smoke(),
                Thunder thunder => thunder,
                Wind => new Natural(),
                _ => null
            };

            return resultantElement;
        }
    }
}
