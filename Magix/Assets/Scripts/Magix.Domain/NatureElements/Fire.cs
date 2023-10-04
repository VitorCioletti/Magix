namespace Magix.Domain.NatureElements
{
    using Interface.NatureElements;

    public class Fire : BaseNatureElement, IFire
    {
        public override NatureElementEffect Effect => NatureElementEffect.OnFire;

        public override INatureElement GetMixedElement(INatureElement natureElement)
        {
            INatureElement resultantNatureElement = natureElement switch
            {
                Water => new Smoke(),
                _ => null
            };

            return resultantNatureElement;
        }
    }
}
