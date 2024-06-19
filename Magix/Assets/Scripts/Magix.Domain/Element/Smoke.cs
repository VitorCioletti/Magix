namespace Magix.Domain.Element
{
    using Interface;
    using Interface.Element;
    using Interface.Element.Result;

    public class Smoke : BaseElement, ISmoke
    {
        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return wizard.SetBlind();
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
