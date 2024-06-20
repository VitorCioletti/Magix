namespace Magix.Domain.Interface.Board.Result
{
    using Element.Result;

    public interface IAttackResult : IResult
    {
        static readonly string NoWizardInPosition = "no-wizard-in-position";

        IWizard Target { get; }

        IEffectResult EffectResult { get; }
    }
}
