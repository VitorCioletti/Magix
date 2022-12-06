namespace Magix.Domain.Board.Result
{
    using System.Collections.Generic;
    using Interface;
    using Interface.Board;
    using Interface.Board.Result;

    public class ApplyNatureElementResult : BaseResult, IApplyNatureElementResult
    {
        public List<ITile> Tiles { get; private set; }

        public ApplyNatureElementResult(
            List<ITile> tiles,
            IWizard wizard,
            bool success,
            string errorId)
            : base(wizard, success, errorId)
        {
            Tiles = tiles;
        }
    }
}
