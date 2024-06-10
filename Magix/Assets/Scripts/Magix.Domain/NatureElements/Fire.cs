namespace Magix.Domain.NatureElements
{
    using Interface;
    using Interface.NatureElements;
    using Interface.NatureElements.Result;

    public class Fire : BaseNatureElement, IFire
    {
        public const int Damage = 1;

        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return wizard.TakeDamage(Damage);
        }

        public override INatureElement GetMixedElement(INatureElement newElementToMix)
        {
            INatureElement resultantNatureElement = newElementToMix switch
            {
                Water => new Smoke(),
                Wind => new Natural(),
                _ => null
            };

            return resultantNatureElement;
        }
    }
}
