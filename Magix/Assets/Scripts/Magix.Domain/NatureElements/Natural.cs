namespace Magix.Domain.NatureElements
{
    using Interface.NatureElements;

    public class Natural : BaseNatureElement
    {
        public override NatureElementEffect Effect => NatureElementEffect.None;

        public override INatureElement ApplyNatureElement(INatureElement natureElement) => natureElement;
    }
}
