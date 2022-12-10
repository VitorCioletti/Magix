namespace Magix.Controller.Match.Board.StateMachine.States
{
    using Domain.Interface;
    using Domain.Interface.NatureElements;
    using NatureElements;
    using Service.Interface;

    public class SelectingNatureElementToCastState : BaseState
    {
        private readonly IWizard _wizard;

        public SelectingNatureElementToCastState(IWizard wizard)
        {
            _wizard = wizard;
        }

        public override void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {
            base.Initialize(stateMachineManager, boardController, matchService);

            BoardController.EnableNatureElementsMenuBar(true);
        }

        public override void Cleanup()
        {
            BoardController.EnableNatureElementsMenuBar(false);
        }

        public override void OnClickNatureElementButton(NatureElementButtonController natureElementButtonController)
        {
            INatureElement natureElement = natureElementButtonController.NatureElement;

            StateMachineManager.Swap(new SelectingTargetToCastNatureElementState(_wizard, natureElement));
        }
    }
}
