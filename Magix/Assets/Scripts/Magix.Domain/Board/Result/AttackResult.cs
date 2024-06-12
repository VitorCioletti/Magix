namespace Magix.Domain.Board.Result
{
    using Interface.Board.Result;
    using Interface.NatureElements.Result;

    public class AttackResult : BaseResult, IAttackResult
    {
        public IEffectResult EffectResult { get; private set; }

        public AttackResult(bool success, IEffectResult result, string errorId = "") : base(success, errorId)
        {
            EffectResult = result;
        }
    }
}
