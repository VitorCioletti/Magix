namespace Magix.Domain.Board.Result
{
    using System.Collections.Generic;
    using Interface.Board;
    using Interface.Board.Result;

    public class MovementResult : BaseResult, IMovementResult
    {
        public string WizardId { get; private set; }

        public List<ITile> Moves { get; private set; }

        public MovementResult(
            List<ITile> moves,
            string wizardId,
            bool success,
            string errorId)
            : base(success, errorId)
        {
            Moves = moves;
            WizardId = wizardId;
        }
    }
}
