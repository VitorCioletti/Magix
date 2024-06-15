namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Result;
    using Service.Interface;

    public class SelectingTilesPathState : BaseState
    {
        private readonly IWizard _wizard;

        private IList<ITile> _selectedTiles = new List<ITile>();
        private IList<ITile> _previewTiles = new List<ITile>();
        private bool _cancelled;

        public SelectingTilesPathState(IWizard wizard)
        {
            _wizard = wizard;
        }

        public override void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {

            base.Initialize(stateMachineManager, boardController, matchService);

            _previewTiles = MatchService.Board.GetPreviewArea(_wizard, WizardActionType.Move);

            _setTilesToPreview(_previewTiles);
        }

        public override BaseStateResult Cleanup()
        {
            _setTilesToNormal(_selectedTiles);
            _setTilesToNormal(_previewTiles);

            return new SelectedTilesResult(_selectedTiles, _cancelled);
        }

        public override void OnClickCancel()
        {
            _cancelled = true;

            Pop();
        }

        public override void OnClickTile(TileController tileController)
        {
            Pop();
        }

        public override void OnEnterMouse(TileController tileController)
        {
            _setTilesToPreview(_previewTiles);

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

        private void _setTilesToPreview(IList<ITile> previewTiles)
        {
            foreach (ITile tile in previewTiles)
            {
                IPosition position = tile.Position;

                BoardController.GridController.Tiles[position.X, position.Y].SetToPreview();
            }
        }

        private void _setTilesToNormal(IList<ITile> tiles)
        {
            foreach (ITile selectedTile in tiles)
            {
                IPosition position = selectedTile.Position;

                TileController tileController = BoardController.GridController.Tiles[position.X, position.Y];

                tileController.SetToNormal();
            }
        }
    }
}
