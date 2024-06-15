namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System.Collections.Generic;
    using Domain;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Result;
    using Service.Interface;

    public class SelectingTilesIndividuallyState : BaseState
    {
        private readonly IWizard _wizard;

        private IList<ITile> _previewTiles = new List<ITile>();
        private readonly IList<ITile> _selectedTiles = new List<ITile>();
        private readonly WizardActionType _wizardAction;
        private bool _cancelled;

        public SelectingTilesIndividuallyState(IWizard wizard, WizardActionType wizardAction)
        {
            _wizardAction = wizardAction;
            _wizard = wizard;
        }

        public override void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {
            base.Initialize(stateMachineManager, boardController, matchService);

            _previewTiles = MatchService.Board.GetPreviewArea(_wizard, _wizardAction);

            BoardController.EnableExecuteButton(true);

            _setTilesToPreview(_previewTiles);
        }

        public override BaseStateResult Cleanup()
        {
            BoardController.EnableExecuteButton(false);

            _setTilesToNormal(_selectedTiles);
            _setTilesToNormal(_previewTiles);

            return new SelectedTilesResult(_selectedTiles, _cancelled);
        }

        public override void OnClickCancel()
        {
            _cancelled = true;

            Pop();
        }

        public override void OnClickExecute()
        {
            Pop();
        }

        public override void OnClickTile(TileController tileController)
        {
            if (!_previewTiles.Contains(tileController.Tile))
                return;

            if (_selectedTiles.Contains(tileController.Tile))
            {
                tileController.SetToPreview();
                _selectedTiles.Remove(tileController.Tile);
            }
            else
            {
                tileController.SetToSelected();
                _selectedTiles.Add(tileController.Tile);
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
