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

        public SelectingTargetToMoveWizardState(IWizard wizard, GridController gridController)
        {
            _wizard = wizard;
            _gridController = gridController;
        }

        public override void Initialize(StateMachineManager stateMachineManager, IMatchService matchService)
        {
            base.Initialize(stateMachineManager, matchService);

            List<ITile> tilesToMove = MatchService.Board.GetAreaToMove(_wizard);
            List<IPosition> positionsToMove = tilesToMove.Select(t => t.Position).ToList();

            _selectTiles(positionsToMove);
        }

        public override void Cleanup() => _deselectAllTiles();

        public override void OnClickTile(TileController tileController)
        {
            StateMachineManager.Pop();
        }

        public override void OnEnterMouse(TileController tileController)
        {
            _deselectAllTiles();

            List<IPosition> previewPositionMoves =
                MatchService.Board.GetPreviewPositionMoves(_wizard, tileController.Tile);

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
