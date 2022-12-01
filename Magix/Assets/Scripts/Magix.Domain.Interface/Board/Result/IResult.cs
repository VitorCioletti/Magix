namespace Magix.Domain.Interface.Board.Result
{
    public interface IResult
    {
        bool Success { get; }

        string ErrorId { get; }
    }
}
