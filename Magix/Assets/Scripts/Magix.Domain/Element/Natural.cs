namespace Magix.Domain.Element
{
    using Interface;
    using Interface.Element;
    using Interface.Element.Result;

    public class Natural : BaseElement, INatural
    {
        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return wizard.ClearDebuffs();
        }

        public override IElement GetMixedElement(IElement newElementToMix)
        {
            if (newElementToMix is IElectric)
                return null;

            return newElementToMix;
        }
    }
}
