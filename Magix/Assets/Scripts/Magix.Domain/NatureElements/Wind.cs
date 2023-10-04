namespace Magix.Domain.NatureElements
{
    using Interface.NatureElements;

    public class Wind : BaseNatureElement, IWind
    {
        public override NatureElementEffect Effect => NatureElementEffect.Dry;

        public override INatureElement GetMixedElement(INatureElement natureElement)
        {
            return natureElement.Blocking ? natureElement : new Natural();
        }
    }
}
