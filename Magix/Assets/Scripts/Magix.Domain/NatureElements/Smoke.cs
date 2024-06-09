namespace Magix.Domain.NatureElements
{
    using Interface;
    using Interface.Board;
    using Interface.NatureElements;
    using Interface.NatureElements.Result;

    public class Smoke : BaseNatureElement, ISmoke
    {
        public override void OnCast(ITile tile)
        {
            tile.NatureElements.RemoveAll(e => e is Water);
        }

        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return wizard.SetBlind();
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
