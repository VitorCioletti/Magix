namespace Magix.Controller.Match.Board.StateMachine.States.Result
{
    using System.Collections.Generic;
    using Domain.Interface.Board;

    public class SelectedTilesResult : BaseStateResult
    {
        public bool Cancelled { get; private set; }

        public IList<ITile> Tiles { get; private set; }

        public SelectedTilesResult(IList<ITile> tiles, bool cancelled)
        {
            Tiles = tiles;
            Cancelled = cancelled;
        }
    }
}
