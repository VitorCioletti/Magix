namespace Magix.Domain.Board.Result
{
    using Interface.Board.Result;

    public class BaseResult : IResult
    {
        public bool Success { get; private set; }

        public string ErrorId { get; private set; }

        public BaseResult(bool success, string errorId)
        {
            Success = success;
            ErrorId = errorId;
        }
    }
}
