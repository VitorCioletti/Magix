namespace Magix.Domain.NatureElements
{
    using Interface;
    using Interface.Board;
    using Interface.NatureElements;
    using Interface.NatureElements.Result;

    public class Wind : BaseNatureElement, IWind
    {
        public override void OnCast(ITile tile)
        {
            tile.NatureElements.RemoveAll(e => !e.Blocking);
        }

        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return wizard.ClearDebuffs();
        }

        public override INatureElement GetMixedElement(INatureElement natureElement)
        {
            return natureElement.Blocking ? natureElement : new Natural();
        }
    }
}
