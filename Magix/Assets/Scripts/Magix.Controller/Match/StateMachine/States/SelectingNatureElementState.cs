namespace Magix.Controller.Match.StateMachine.States
{
    using Domain.Interface;
    using Service.Interface;

    public class SelectingNatureElementState : BaseState
    {
        private IWizard _wizard;

        public SelectingNatureElementState(IWizard wizard)
        {
            _wizard = wizard;
        }

        public override void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {
            base.Initialize(stateMachineManager, boardController, matchService);

            StateMachineManager.Swap(new SelectingTargetToApplyNatureElementState(_wizard));
        }
    }
}
