namespace Magix.Domain.Interface.Board.Result
{
    using Element;

    public interface IMixResult : IResult
    {
        // TODO: Tiles should not be visible, only positions.
        ITile AffectedTile { get; }

        IElement TriedToMix { get; }

        IElement OriginallyOnTile { get; }

        IElement NewElement { get; }
    }
}
