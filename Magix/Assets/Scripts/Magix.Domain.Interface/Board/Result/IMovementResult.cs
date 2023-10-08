namespace Magix.Domain.Interface.Board.Result
{
    using System.Collections.Generic;

    public interface IMovementResult : IResult
    {
        List<IStepResult> Steps { get; }
    }
}
