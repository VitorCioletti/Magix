namespace Magix.Domain.NatureElements
{
    public class Smoke : BaseNatureElement
    {
        protected override NatureElementEffect Effect => NatureElementEffect.Blind;

        public override BaseNatureElement ApplyNatureElement(BaseNatureElement natureElement)
        {
            BaseNatureElement newNatureElement = null;

            switch (natureElement)
            {
                case Wind:
                    newNatureElement = new Natural();
                    break;
            }

            return newNatureElement;
        }
    }
}
