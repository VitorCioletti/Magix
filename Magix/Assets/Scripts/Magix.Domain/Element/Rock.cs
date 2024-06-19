namespace Magix.Domain.Element
{
    using Interface;
    using Interface.Element.Result;

    public class Rock : BaseElement
    {
        public override bool Blocking => true;

        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return null;
        }
    }
}
