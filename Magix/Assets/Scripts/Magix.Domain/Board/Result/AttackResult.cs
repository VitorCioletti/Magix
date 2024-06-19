namespace Magix.Domain.Board.Result
{
    using Interface;
    using Interface.Board.Result;
    using Interface.Element.Result;

    public class AttackResult : BaseResult, IAttackResult
    {
        public IWizard Target { get; private set; }

        public IEffectResult EffectResult { get; private set; }

        public AttackResult(
            bool success,
            IEffectResult result,
            IWizard target,
            string errorId = "") : base(success, errorId)
        {
            EffectResult = result;
            Target = target;
        }
    }
}
