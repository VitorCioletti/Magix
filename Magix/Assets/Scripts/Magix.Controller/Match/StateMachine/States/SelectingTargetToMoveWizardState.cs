namespace Magix.Controller.Match.StateMachine.States
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Service.Interface;

    public class SelectingTargetToMoveWizardState : BaseState
    {
        private readonly IWizard _wizard;

        private readonly GridController _gridController;

        private List<ITile> _previewTilesMoves;

        public SelectingTargetToMoveWizardState(IWizard wizard, GridController gridController)
        {
            _wizard = wizard;
            _gridController = gridController;
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

        public override void Cleanup() => _deselectAllTiles();

        public override void OnClickTile(TileController tileController)
        {
            ITile tile = tileController.Tile;

            if (MatchService.Board.HasWizard(tile))
                return;

            StateMachineManager.Swap(new MovingWizardToTargetState(_wizard, _previewTilesMoves));
        }

        public override void OnEnterMouse(TileController tileController)
        {
            _deselectAllTiles();

            _previewTilesMoves =
                MatchService.Board.GetPreviewPositionMoves(_wizard, tileController.Tile);

            List<IPosition> previewPositionMoves = _previewTilesMoves.Select(t => t.Position).ToList();

            _selectTiles(previewPositionMoves);
        }

        private void _selectTiles(List<IPosition> previewPositionMoves)
        {
            foreach (IPosition position in previewPositionMoves)
                _gridController.Tiles[position.X, position.Y].Select();
        }

        private void _deselectAllTiles()
        {
            foreach (TileController tileController in _gridController.Tiles)
                tileController.Deselect();
        }
    }
}
