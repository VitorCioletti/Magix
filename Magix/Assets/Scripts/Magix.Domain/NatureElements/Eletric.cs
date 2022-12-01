namespace Magix.Domain.NatureElements
{
    using Interface.NatureElements;

    public class Eletric : BaseNatureElement
    {
        public override NatureElementEffect Effect => NatureElementEffect.Shocked;

        public override INatureElement ApplyNatureElement(INatureElement natureElement)
        {
            INatureElement resultantBaseNatureElement = null;

            switch (natureElement)
            {
                case Natural:
                    resultantBaseNatureElement = natureElement;
                    break;
            }

            return resultantBaseNatureElement;
        }
    }
}
