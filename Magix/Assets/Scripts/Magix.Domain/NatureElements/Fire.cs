namespace Magix.Domain.NatureElements
{
    using Interface;
    using Interface.Board;
    using Interface.NatureElements;
    using Interface.NatureElements.Result;

    public class Fire : BaseNatureElement, IFire
    {
        public const int Damage = 1;

        public override void OnCast(ITile tile)
        {
            tile.NatureElements.RemoveAll(e => e is Water);
        }

        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return wizard.TakeDamage(Damage);
        }

        public override INatureElement GetMixedElement(INatureElement natureElement)
        {
            INatureElement resultantNatureElement = natureElement switch
            {
                Water => new Smoke(),
                _ => null
            };

            return resultantNatureElement;
        }
    }
}
