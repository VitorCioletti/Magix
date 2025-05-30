﻿namespace Magix.Controller.Match.Board.StateMachine.States
{
    using Domain.Interface;
    using Result;
    using Service.Interface;

    public class IdleState : BaseState
    {
        public override void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {
            base.Initialize(stateMachineManager, boardController, matchService);

            BoardController.EnableCancelButton(false);
        }

        public override void OnGotBackOnTop(BaseStateResult stateResult)
        {
            BoardController.EnableCancelButton(false);
        }

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
            tileController.SetToPreview();
        }

        public override void OnExitMouse(TileController tileController)
        {
            tileController.SetToNormal();
        }
    }
}
