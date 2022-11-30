namespace Magix.Domain.NatureElements
{
    public class Water : BaseNatureElement
    {
        public override NatureElementEffect Effect => NatureElementEffect.Wet;

        public override BaseNatureElement ApplyNatureElement(BaseNatureElement natureElement)
        {
            BaseNatureElement resultantNatureElement = null;

            switch (natureElement)
            {
                case Fire:
                    resultantNatureElement = new Smoke();
                    break;
            }

            return resultantNatureElement;
        }
    }
}
