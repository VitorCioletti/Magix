namespace Magix.Controller.Match.StateMachine.States
{
    using Domain.Interface;
    using Service.Interface;

    public class SelectingActionToWizardPerformState : BaseState
    {
        private IWizard _wizard;

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

        public override void Cleanup() => BoardController.EnableActionSelectionButtons(false);

        public override void OnClickActionMove()
        {
            StateMachineManager.Swap(new SelectingTargetToMoveWizardState(_wizard));
        }

        public override void OnClickActionApplyNatureElement()
        {
            StateMachineManager.Swap(new SelectingNatureElementState(_wizard));
        }
    }
}
