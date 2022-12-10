namespace Magix.Domain.Interface.Board
{
    using NatureElements;

    public interface ITile
    {
        IPosition Position { get; }

        INatureElement NatureElement { get; }

        void CastNatureElement(INatureElement natureElement);
    }
}
