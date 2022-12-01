namespace Magix.Domain.NatureElements
{
    using Interface.NatureElements;

    public class Water : BaseNatureElement
    {
        public override NatureElementEffect Effect => NatureElementEffect.Wet;

        public override INatureElement ApplyNatureElement(INatureElement natureElement)
        {
            BaseNatureElement resultantBaseNatureElement = null;

            switch (natureElement)
            {
                case Fire:
                    resultantBaseNatureElement = new Smoke();
                    break;
            }

            return resultantBaseNatureElement;
        }
    }
}
