namespace Magix.Domain.NatureElements
{
    using Interface.NatureElements;

    public class Fire : BaseNatureElement, IFire
    {
        public override NatureElementEffect Effect => NatureElementEffect.OnFire;

        public override INatureElement CastNatureElement(INatureElement natureElement)
        {
            INatureElement resultantNatureElement = null;

            switch (natureElement)
            {
                case Water:
                    resultantNatureElement = new Smoke();
                    break;
            }

            return resultantNatureElement;
        }
    }
}
