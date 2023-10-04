namespace Magix.Domain.Interface.Board.Result
{
    using System.Collections.Generic;

    public interface ICastResult : IResult
    {
        List<IMixResult> ResultedMixes { get; }
    }
}
