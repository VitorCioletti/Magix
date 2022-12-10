namespace Magix.Controller.Match.Board.StateMachine.States
{
    using Domain.Interface;

    public class IdleState : BaseState
    {
        public override void OnClickTile(TileController tileController)
        {
            IWizard wizard = MatchService.Board.GetWizard(tileController.Tile);

            if (!MatchService.Board.BelongsToCurrentPlayer(wizard))
                return;

            if (wizard == null)
                return;

            if (!wizard.HasRemainingActions())
                return;

            StateMachineManager.Push(new SelectingActionToWizardPerformState(wizard));
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
