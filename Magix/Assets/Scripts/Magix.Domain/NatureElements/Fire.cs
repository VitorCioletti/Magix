namespace Magix.Domain.NatureElements
{
    public class Fire : BaseNatureElement
    {
        public override NatureElementEffect Effect => NatureElementEffect.OnFire;

        public override BaseNatureElement ApplyNatureElement(BaseNatureElement natureElement)
        {
            BaseNatureElement resultantNatureElement = null;

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
