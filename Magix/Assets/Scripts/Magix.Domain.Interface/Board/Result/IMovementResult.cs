namespace Magix.Domain.Interface.Board.Result
{
    using System.Collections.Generic;

    public interface IMovementResult : IResult
    {
        // TODO: Create IStepResult to put apply effect results.
        List<ITile> Moves { get; }
    }
}
