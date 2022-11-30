namespace Magix.Domain.NatureElements
{
    public class Smoke : BaseNatureElement
    {
        public override NatureElementEffect Effect => NatureElementEffect.Blind;

        public override BaseNatureElement ApplyNatureElement(BaseNatureElement natureElement)
        {
            BaseNatureElement resultantNatureElement = null;

            switch (natureElement)
            {
                case Wind:
                    resultantNatureElement = new Natural();
                    break;
            }

            return resultantNatureElement;
        }
    }
}
