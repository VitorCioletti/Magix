namespace Magix.Domain.NatureElements
{
    using Interface.NatureElements;

    public class Water : BaseNatureElement, IWater
    {
        public override NatureElementEffect Effect => NatureElementEffect.Wet;

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
