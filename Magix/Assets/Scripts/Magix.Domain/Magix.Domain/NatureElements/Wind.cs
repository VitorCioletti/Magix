namespace Magix.Domain.NatureElements
{
    public class Wind : BaseNatureElement
    {
        protected override NatureElementEffect Effect => NatureElementEffect.Dry;

        public override BaseNatureElement ApplyNatureElement(BaseNatureElement natureElement) => new Natural();
    }
}
