namespace Magix.Domain.Element
{
    using Interface;
    using Interface.Element;
    using Interface.Element.Result;

    public class Wind : BaseElement, IWind
    {
        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return wizard.ClearDebuffs();
        }
    }
}
