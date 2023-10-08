namespace Magix.Domain.NatureElements
{
    using Interface;
    using Interface.Board;
    using Interface.NatureElements;
    using Interface.NatureElements.Result;

    public class Fire : BaseNatureElement, IFire
    {
        private const int _damage = 1;

        public override void OnCast(ITile tile)
        {
            tile.Elements.RemoveAll(e => e is Water);
        }

        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return wizard.TakeDamage(_damage);
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
