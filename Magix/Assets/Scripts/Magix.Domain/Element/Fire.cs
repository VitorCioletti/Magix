namespace Magix.Domain.Element
{
    using Interface;
    using Interface.Element;
    using Interface.Element.Result;

    public class Fire : BaseElement, IFire
    {
        public const int Damage = 1;

        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return wizard.TakeDamage(Damage);
        }

        public override IElement GetMixedElement(IElement newElementToMix)
        {
            IElement resultantElement = newElementToMix switch
            {
                Water => new Smoke(),
                Wind => new Natural(),
                _ => null
            };

            return resultantElement;
        }
    }
}
