namespace Magix.Domain.Element
{
    using Interface;
    using Interface.Element;
    using Interface.Element.Result;

    public class Thunder : BaseElement, IElectric
    {
        public override bool CanSpread => true;

        public override bool CanStack => true;

        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return wizard.Stun();
        }

        public override IElement GetMixedElement(IElement newElementToMix)
        {
            IElement resultantElement = null;

            switch (newElementToMix)
            {
                case Wind:
                    resultantElement = new Natural();

                    break;
            }

            return resultantElement;
        }
    }
}
