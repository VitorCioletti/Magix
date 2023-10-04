namespace Magix.Domain.NatureElements
{
    using Interface.NatureElements;

    public class Rock : BaseNatureElement
    {
        public override bool Blocking => true;

        public override NatureElementEffect Effect => NatureElementEffect.None;

        public override INatureElement GetMixedElement(INatureElement natureElement)
        {
            return null;
        }
    }
}
