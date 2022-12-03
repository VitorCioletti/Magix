namespace Magix.Controller.Match.StateMachine.States
{
    using System.Collections.Generic;
    using DependencyInjection;
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

        public override void OnClickTile(TileController tileController)
        {
            StateMachineManager.Pop();
        }

        public override void OnMouseEntered(TileController tileController)
        {
            _deselectAllTiles();

            var matchService = Resolver.GetService<IMatchService>();

            List<IPosition> previewPositionMoves =
                matchService.Board.GetPreviewPositionMoves(_wizard, tileController.Tile);

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
