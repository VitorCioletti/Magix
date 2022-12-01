namespace Magix.Domain.Interface.Board.Result
{
    using System.Collections.Generic;

    public interface IMovementResult : IResult
    {
        string WizardId { get; }

        List<ITile> Moves { get; }
    }
}
