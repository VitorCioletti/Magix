namespace Magix.Domain.Interface.Board.Result
{
    using NatureElements;

    public interface IMixResult : IResult
    {
        // TODO: Tiles should not be visible, only positions.
        ITile AffectedTile { get; }

        INatureElement TriedToMix { get; }

        INatureElement OriginallyOnTile { get; }

        INatureElement NewElement { get; }
    }
}
