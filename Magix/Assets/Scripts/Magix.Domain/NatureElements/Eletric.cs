namespace Magix.Domain.NatureElements
{
    using Interface.NatureElements;

    public class Eletric : BaseNatureElement, IEletric
    {
        public override NatureElementEffect Effect => NatureElementEffect.Shocked;

        public override INatureElement GetMixedElement(INatureElement natureElement)
        {
            INatureElement resultantNatureElement = null;

            switch (natureElement)
            {
                case Natural:
                    resultantNatureElement = new Natural();
                    break;
            }

            return resultantNatureElement;
        }
    }
}
