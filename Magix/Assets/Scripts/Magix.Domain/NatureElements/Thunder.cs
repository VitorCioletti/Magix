namespace Magix.Domain.NatureElements
{
    using Interface;
    using Interface.NatureElements;
    using Interface.NatureElements.Result;

    public class Thunder : BaseNatureElement, IElectric
    {
        public override bool CanSpread => true;
        public override bool CanStack => true;

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
