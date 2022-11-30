namespace Magix.Domain.NatureElements
{
    public class Natural : BaseNatureElement
    {
        public override NatureElementEffect Effect => NatureElementEffect.None;

        public override BaseNatureElement ApplyNatureElement(BaseNatureElement natureElement) => natureElement;
    }
}
