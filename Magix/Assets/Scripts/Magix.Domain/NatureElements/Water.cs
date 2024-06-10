namespace Magix.Domain.NatureElements
{
    using Interface;
    using Interface.NatureElements;
    using Interface.NatureElements.Result;

    public class Water : BaseNatureElement, IWater
    {
        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return null;
        }

        public override INatureElement GetMixedElement(INatureElement newElementToMix)
        {
            INatureElement resultantNatureElement = newElementToMix switch
            {
                Fire => new Smoke(),
                Thunder thunder => thunder,
                Wind => new Natural(),
                _ => null
            };

            return resultantNatureElement;
        }
    }
}
