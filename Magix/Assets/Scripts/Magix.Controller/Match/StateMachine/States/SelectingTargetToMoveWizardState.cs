namespace Magix.Controller.Match.StateMachine.States
{
    using System.Collections.Generic;
    using System.Linq;
    using DependencyInjection;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Service.Interface;

    public class SelectingTargetToMoveWizardState : BaseState
    {
        private readonly IWizard _wizard;

        private readonly GridController _gridController;

        private readonly IMatchService _matchService;

        public SelectingTargetToMoveWizardState(IWizard wizard, GridController gridController)
        {
            _wizard = wizard;
            _gridController = gridController;
            _matchService = Resolver.GetService<IMatchService>();
        }

        public override void Initialize(StateMachineManager stateMachineManager)
        {
            base.Initialize(stateMachineManager);

            List<ITile> tilesToMove = _matchService.Board.GetAreaToMove(_wizard);
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
                _matchService.Board.GetPreviewPositionMoves(_wizard, tileController.Tile);

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
