namespace Magix.Controller.Match.Board.StateMachine.States.Result
{
    using System.Collections.Generic;
    using Domain.Interface.Board;

    public class SelectedTilesResult : BaseStateResult
    {
        public IList<ITile> Tiles { get; private set; }

        public SelectedTilesResult(IList<ITile> tiles)
        {
            Tiles = tiles;
        }
    }
}
