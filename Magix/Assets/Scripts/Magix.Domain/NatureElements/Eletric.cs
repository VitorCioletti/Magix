namespace Magix.Domain.NatureElements
{
    using Interface;
    using Interface.NatureElements;
    using Interface.NatureElements.Result;
    using Result;

    public class Eletric : BaseNatureElement, IEletric
    {
        public override bool CanSpread => true;

        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return wizard.Stun();
        }

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
