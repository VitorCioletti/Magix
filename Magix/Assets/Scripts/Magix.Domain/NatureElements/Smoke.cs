namespace Magix.Domain.NatureElements
{
    using Interface.NatureElements;

    public class Smoke : BaseNatureElement
    {
        public override NatureElementEffect Effect => NatureElementEffect.Blind;

        public override INatureElement ApplyNatureElement(INatureElement natureElement)
        {
            INatureElement resultantBaseNatureElement = null;

            switch (natureElement)
            {
                case Wind:
                    resultantBaseNatureElement = new Natural();
                    break;
            }

            return resultantBaseNatureElement;
        }
    }
}
