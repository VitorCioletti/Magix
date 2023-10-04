namespace Magix.Domain.Board.Result
{
    using Interface.Board;
    using Interface.Board.Result;
    using Interface.NatureElements;

    public class MixResult : BaseResult, IMixResult
    {
        public ITile AffectedTile { get; private set; }

        public INatureElement NewElement { get; private set; }

        public INatureElement TriedToMix { get; private set; }

        public INatureElement OriginallyOnTile { get; private set; }

        public MixResult(
            ITile affectedTile,
            INatureElement triedToMix,
            INatureElement originallyOnTile,
            INatureElement newElement,
            bool success = true,
            string errorId = "")
            : base(success, errorId)
        {
            AffectedTile = affectedTile;
            TriedToMix = triedToMix;
            OriginallyOnTile = originallyOnTile;
            NewElement = newElement;
        }
    }
}
