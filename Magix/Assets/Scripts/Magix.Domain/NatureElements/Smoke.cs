namespace Magix.Domain.NatureElements
{
    using Interface;
    using Interface.NatureElements;
    using Interface.NatureElements.Result;

    public class Smoke : BaseNatureElement, ISmoke
    {
        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return wizard.SetBlind();
        }

        public override INatureElement GetMixedElement(INatureElement newElementToMix)
        {
            INatureElement resultantNatureElement = null;

            switch (newElementToMix)
            {
                case Wind:
                    resultantNatureElement = new Natural();

                    break;
            }

            return resultantNatureElement;
        }
    }
}
