namespace Magix.Domain.NatureElements
{
    using Interface;
    using Interface.NatureElements.Result;

    public class Rock : BaseNatureElement
    {
        public override bool Blocking => true;

        public override IEffectResult ApplyElementEffect(IWizard wizard)
        {
            return null;
        }
    }
}
