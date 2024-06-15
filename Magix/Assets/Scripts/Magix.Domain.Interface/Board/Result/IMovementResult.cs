namespace Magix.Domain.Interface.Board.Result
{
    using System.Collections.Generic;

    public interface IMovementResult : IResult
    {
        static string CantGoThroughBlockingElement = "cant-go-through-blocking-element";

        List<IStepResult> Steps { get; }
    }
}
