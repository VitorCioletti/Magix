namespace Magix.Domain.NatureElements
{
    using Interface.NatureElements;

    public class Smoke : BaseNatureElement, ISmoke
    {
        public override NatureElementEffect Effect => NatureElementEffect.Blind;

        public override INatureElement ApplyNatureElement(INatureElement natureElement)
        {
            INatureElement resultantNatureElement = null;

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
