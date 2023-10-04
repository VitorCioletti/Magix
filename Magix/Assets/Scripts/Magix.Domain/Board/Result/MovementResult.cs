namespace Magix.Domain.Board.Result
{
    using System.Collections.Generic;
    using Interface;
    using Interface.Board;
    using Interface.Board.Result;

    public class MovementResult : BaseResult, IMovementResult
    {
        public List<ITile> Moves { get; private set; }

        public MovementResult(
            List<ITile> moves,
            bool success,
            string errorId)
            : base(success, errorId)
        {
            Moves = moves;
        }
    }
}
