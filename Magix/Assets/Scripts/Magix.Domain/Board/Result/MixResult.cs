namespace Magix.Domain.Board.Result
{
    using Interface.Board;
    using Interface.Board.Result;
    using Interface.Element;

    public class MixResult : BaseResult, IMixResult
    {
        public ITile AffectedTile { get; private set; }

        public IElement NewElement { get; private set; }

        public IElement TriedToMix { get; private set; }

        public IElement OriginallyOnTile { get; private set; }

        public MixResult(
            ITile affectedTile,
            IElement triedToMix,
            IElement originallyOnTile,
            IElement newElement,
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
