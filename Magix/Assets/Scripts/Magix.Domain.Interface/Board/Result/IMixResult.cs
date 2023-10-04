namespace Magix.Domain.Interface.Board.Result
{
    using NatureElements;

    public interface IMixResult : IResult
    {
        ITile AffectedTile { get; }

        INatureElement TriedToMix { get; }

        INatureElement OriginallyOnTile { get; }
    }
}
