namespace Magix.Domain.Board.Result
{
    using System.Collections.Generic;
    using Interface.Board.Result;

    public class CastResult : BaseResult, ICastResult
    {
        public List<IMixResult> ResultedMixes { get; private set; }

        public CastResult(string errorId) : base(false, errorId)
        {
            ResultedMixes = new List<IMixResult>();
        }

        public CastResult(List<IMixResult> resultedMixes) : base(true, string.Empty)
        {
            Success = resultedMixes.Exists(r => r.Success);

            ResultedMixes = resultedMixes;
        }
    }
}
