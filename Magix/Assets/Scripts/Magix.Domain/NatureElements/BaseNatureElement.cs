namespace Magix.Domain.NatureElements
{
    using Interface;
    using Interface.Board;
    using Interface.NatureElements;
    using Interface.NatureElements.Result;

    public abstract class BaseNatureElement : INatureElement
    {
        public virtual bool Blocking => false;

        public virtual bool CanSpread => false;

        public abstract IEffectResult ApplyElementEffect(IWizard wizard);

        public virtual INatureElement GetMixedElement(INatureElement natureElement)
        {
            return null;
        }

        public virtual void OnCast(ITile tile)
        {

        }

        public bool CanReact(INatureElement natureElement)
        {
            return GetMixedElement(natureElement) is not null;
        }
    }
}
