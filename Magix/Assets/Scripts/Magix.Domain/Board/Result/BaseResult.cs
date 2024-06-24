namespace Magix.Domain.Board.Result
{
    using Interface;
    using Interface.Board.Result;

    public abstract class BaseResult : IResult
    {
        public bool Success { get; protected set; }

        public string ErrorId { get; private set; }

        public IPlayer Winner { get; private set; }

        public bool GameEnded => Winner is not null;

        protected BaseResult(bool success, string errorId)
        {
            Success = success;
            ErrorId = errorId;
        }

        public void SetWinner(IPlayer player)
        {
            Winner = player;
        }
    }
}
