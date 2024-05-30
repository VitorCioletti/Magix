namespace Magix.Domain.NatureElements
{
    using Interface;
    using Interface.NatureElements;
    using Interface.NatureElements.Result;

    public class Natural : BaseNatureElement, INatural
    {
        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return wizard.ClearDebuffs();
        }

        public override INatureElement GetMixedElement(INatureElement natureElement)
        {
            if (natureElement is IEletric)
                return null;

            return natureElement;
        }
    }
}
