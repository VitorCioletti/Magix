namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System;
    using Domain.Interface;
    using Domain.Interface.NatureElements;
    using NatureElements;
    using Result;
    using Service.Interface;

    public class SelectingNatureElementToCastState : BaseState
    {
        private readonly IWizard _wizard;
        private INatureElement _selectedNatureElement;

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

        public override void OnGotBackOnTop(BaseStateResult stateResult)
        {
            switch (stateResult)
            {
                case SelectedTilesResult selectedTilesResult:
                    StateMachineManager.Swap(
                        new CastingNatureElementState(_wizard, _selectedNatureElement, selectedTilesResult.Tiles));

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stateResult));
            }

        }

        public override BaseStateResult Cleanup()
        {
            BoardController.EnableNatureElementsMenuBar(false);

            return null;
        }

        public override void OnClickNatureElement(NatureElementButtonController natureElementButtonController)
        {
            _selectedNatureElement = natureElementButtonController.NatureElement;

            StateMachineManager.Push(new SelectingTilesState(_wizard));
        }
    }
}
