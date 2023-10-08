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

        public override INatureElement GetMixedElement(INatureElement natureElement)
        {
            INatureElement resultantNatureElement = natureElement switch
            {
                Fire => new Smoke(),
                Eletric => natureElement,
                _ => null
            };

            return resultantNatureElement;
        }
    }
}
