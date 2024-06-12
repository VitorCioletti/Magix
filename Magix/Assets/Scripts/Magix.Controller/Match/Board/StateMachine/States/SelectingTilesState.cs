namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Result;
    using Service.Interface;

    public class SelectingTilesState : BaseState
    {
        private readonly IWizard _wizard;
        private List<ITile> _selectedTiles;

        public SelectingTilesState(IWizard wizard)
        {
            _wizard = wizard;
        }

        public override void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {
            base.Initialize(stateMachineManager, boardController, matchService);

            List<ITile> tilesToMove = MatchService.Board.GetAreaToMove(_wizard);
            List<IPosition> positionsToMove = tilesToMove.Select(t => t.Position).ToList();

            _selectTiles(positionsToMove);
        }

        public override BaseStateResult Cleanup()
        {
            return new SelectedTilesResult(_selectedTiles);
        }

        public override void OnClickExecute()
        {
            Pop();
        }

        public override void OnEnterMouse(TileController tileController)
        {
            _deselectAllTiles();

            _selectedTiles =
                MatchService.Board.GetPreviewPositionMoves(_wizard, tileController.Tile);

            List<IPosition> previewPositionMoves = _selectedTiles.Select(t => t.Position).ToList();

            _selectTiles(previewPositionMoves);
        }

        private void _selectTiles(List<IPosition> previewPositionMoves)
        {
            foreach (IPosition position in previewPositionMoves)
                BoardController.GridController.Tiles[position.X, position.Y].Select();
        }

        private void _deselectAllTiles()
        {
            foreach (TileController tileController in BoardController.GridController.Tiles)
                tileController.Deselect();
        }
    }
}
