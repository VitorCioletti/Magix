namespace Magix.Domain.NatureElements
{
    using Interface.NatureElements;

    public class Fire : BaseNatureElement
    {
        public override NatureElementEffect Effect => NatureElementEffect.OnFire;

        public override INatureElement ApplyNatureElement(INatureElement natureElement)
        {
            INatureElement resultantBaseNatureElement = null;

            switch (natureElement)
            {
                case Water:
                    resultantBaseNatureElement = new Smoke();
                    break;
            }

            return resultantBaseNatureElement;
        }
    }
}
