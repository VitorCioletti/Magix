namespace Magix.Domain.Board.Result
{
    using System.Collections.Generic;
    using Interface.Board.Result;

    public class MovementResult : BaseResult, IMovementResult
    {

        public List<IStepResult> Steps { get; private set; }

        public MovementResult(
            List<IStepResult> steps,
            bool success,
            string errorId)
            : base(success, errorId)
        {
            Steps = steps;
        }
    }
}
