namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Result;
    using Service.Interface;

    public class SelectingPathTilesState : BaseState
    {
        private readonly IWizard _wizard;
        private IList<ITile> _selectedTiles;

        public SelectingPathTilesState(IWizard wizard)
        {
            _wizard = wizard;
        }

        public override void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {
            base.Initialize(stateMachineManager, boardController, matchService);

            IList<ITile> tilesToMove = MatchService.Board.GetPreviewArea(_wizard);
            IList<IPosition> positionsToMove = tilesToMove.Select(t => t.Position).ToList();

            _previewTiles(positionsToMove);
        }

        public override BaseStateResult Cleanup()
        {
            _setAllTilesToNormal();

            return new SelectedTilesResult(_selectedTiles);
        }

        public override void OnClickExecute()
        {
            Pop();
        }

        public override void OnClickTile(TileController tileController)
        {
            Pop();
        }

        public override void OnEnterMouse(TileController tileController)
        {
            _setAllTilesToNormal();

            _selectedTiles = MatchService.Board.GetPreviewPathTo(_wizard, tileController.Tile);

            List<IPosition> previewPositionMoves = _selectedTiles.Select(t => t.Position).ToList();

            _selectTiles(previewPositionMoves);
        }

        private void _selectTiles(List<IPosition> tiles)
        {
            foreach (IPosition position in tiles)
            {
                BoardController.GridController.Tiles[position.X, position.Y].SetToSelected();
            }
        }

        private void _previewTiles(IList<IPosition> previewTiles)
        {
            foreach (IPosition position in previewTiles)
            {
                BoardController.GridController.Tiles[position.X, position.Y].SetToPreview();
            }
        }

        private void _setAllTilesToNormal()
        {
            foreach (TileController tileController in BoardController.GridController.Tiles)
            {
                tileController.SetToNormal();
            }
        }
    }
}
