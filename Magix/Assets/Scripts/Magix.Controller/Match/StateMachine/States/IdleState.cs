namespace Magix.Controller.Match.StateMachine.States
{
    using Domain.Interface;

    public class IdleState : BaseState
    {
        private readonly GridController _gridController;

        public IdleState(GridController gridController)
        {
            _gridController = gridController;
        }

        public override void OnClickTile(TileController tileController)
        {
            IWizard wizard = MatchService.Board.GetWizard(tileController.Tile);

            if (!MatchService.Board.BelongsToCurrentPlayer(wizard))
                return;

            if (wizard == null)
                return;

            if (wizard.RemainingActions == 0)
                return;

            var selectingTargetToMoveWizardState = new SelectingTargetToMoveWizardState(wizard, _gridController);

            StateMachineManager.Push(selectingTargetToMoveWizardState);
        }

        public override void OnEnterMouse(TileController tileController)
        {
            tileController.Select();
        }

        public override void OnExitMouse(TileController tileController)
        {
            tileController.Deselect();
        }
    }
}
