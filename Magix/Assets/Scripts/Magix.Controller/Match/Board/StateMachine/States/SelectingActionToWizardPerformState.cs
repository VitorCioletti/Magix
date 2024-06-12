namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System;
    using Domain.Interface;
    using Result;
    using Service.Interface;

    public class SelectingActionToWizardPerformState : BaseState
    {
        private readonly IWizard _wizard;

        public SelectingActionToWizardPerformState(IWizard wizard)
        {
            _wizard = wizard;
        }

        public override void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {
            base.Initialize(stateMachineManager, boardController, matchService);

            BoardController.EnableActionSelectionButtons(true);
        }

        public override BaseStateResult Cleanup()
        {
            BoardController.EnableActionSelectionButtons(false);

            return null;
        }

        public override void OnClickWizardAction(WizardAction action)
        {
            switch (action)
            {
                case WizardAction.Attack:
                    StateMachineManager.Swap(new SelectingTilesToAttackState(_wizard));

                    break;
                case WizardAction.Move:
                    StateMachineManager.Swap(new SelectingTilesToMoveState(_wizard));

                    break;
                case WizardAction.CastNatureElement:
                    StateMachineManager.Swap(new SelectingNatureElementToCastState(_wizard));

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }
    }
}
