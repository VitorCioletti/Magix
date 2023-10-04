namespace Magix.Domain.Interface.NatureElements
{
    using Board.Result;

    public interface INatureElement
    {
        NatureElementEffect Effect { get; }

        bool Blocking { get; }

        bool CanSpread { get; }

        void ApplyElementEffect(IWizard wizard);

        INatureElement GetMixedElement(INatureElement natureElement);

        bool CanReact(INatureElement natureElement);
    }
}
