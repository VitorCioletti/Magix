namespace Magix.Domain.Interface.Board.Result
{
    using NatureElements.Result;

    public interface IAttackResult
    {
        static string NoWizardInPosition = "no-wizard-in-position";

        IEffectResult EffectResult { get; }
    }
}
