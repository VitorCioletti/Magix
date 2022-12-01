namespace Magix.Domain.Interface.Board
{
    using NatureElements;

    public interface ITile
    {
        IPosition Position { get; }

        INatureElement BaseNatureElement { get; }

        void ApplyNatureElement(INatureElement natureElement);
    }
}
