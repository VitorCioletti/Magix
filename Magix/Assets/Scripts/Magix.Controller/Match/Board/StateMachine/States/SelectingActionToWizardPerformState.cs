namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System;
    using Domain;
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

            BoardController.EnableActionSelectionButtons(_wizard, true);
        }

        public override BaseStateResult Cleanup()
        {
            BoardController.EnableActionSelectionButtons(false);

            return null;
        }

        public override void OnClickWizardAction(WizardActionType actionType)
        {
            switch (actionType)
            {
                case WizardActionType.Attack:
                    StateMachineManager.Swap(new SelectingTilesToAttackState(_wizard));

                    break;
                case WizardActionType.Move:
                    StateMachineManager.Swap(new SelectingTilesToMoveState(_wizard));

                    break;
                case WizardActionType.CastElement:
                    StateMachineManager.Swap(new SelectingElementToCastState(_wizard));

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null);
            }
        }
    }
}
