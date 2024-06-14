namespace Magix.Domain.Interface.Board.Result
{
    using NatureElements.Result;

    public interface IAttackResult : IResult
    {
        static string NoWizardInPosition = "no-wizard-in-position";

        IWizard Target { get; }

        IEffectResult EffectResult { get; }
    }
}
