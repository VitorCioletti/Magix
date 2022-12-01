namespace Magix.Service.Interface
{
    using Domain.Interface;
    using Domain.Interface.Board;

    public interface IMatchService
    {
        IBoard Board { get; }

        IPlayer StartNew();
    }
}
