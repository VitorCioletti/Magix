namespace Magix.Domain.NatureElements
{
    public class Water : BaseNatureElement
    {
        protected override NatureElementEffect Effect => NatureElementEffect.Wet;

        public override BaseNatureElement ApplyNatureElement(BaseNatureElement natureElement)
        {
            BaseNatureElement newNatureElement = null;

            switch (natureElement)
            {
                case Fire:
                    newNatureElement = new Smoke();
                    break;
            }

            return newNatureElement;
        }
    }
}
