namespace Magix.Domain.NatureElements
{
    using Interface;
    using Interface.NatureElements;
    using Interface.NatureElements.Result;

    public class Wind : BaseNatureElement, IWind
    {
        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return wizard.ClearDebuffs();
        }
    }
}
