namespace Magix.Domain.Interface.Board
{
    public interface IPosition
    {
        int X { get; }

        int Y { get; }

        bool Equals(IPosition position);
    }
}
