namespace Magix.Domain.NatureElements
{
    using Interface.NatureElements;

    public class Water : BaseNatureElement, IWater
    {
        public override NatureElementEffect Effect => NatureElementEffect.Wet;

        public override INatureElement ApplyNatureElement(INatureElement natureElement)
        {
            INatureElement resultantNatureElement = null;

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
