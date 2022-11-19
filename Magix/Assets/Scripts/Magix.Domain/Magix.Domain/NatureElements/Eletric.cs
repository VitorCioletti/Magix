namespace Magix.Domain.NatureElements
{
    public class Eletric : BaseNatureElement
    {
        protected override NatureElementEffect Effect => NatureElementEffect.Shocked;

        public override BaseNatureElement ApplyNatureElement(BaseNatureElement natureElement)
        {
            BaseNatureElement resultantNatureElement = null;

            switch (natureElement)
            {
                case Natural:
                    resultantNatureElement = natureElement;
                    break;
            }

            return resultantNatureElement;
        }
    }
}
