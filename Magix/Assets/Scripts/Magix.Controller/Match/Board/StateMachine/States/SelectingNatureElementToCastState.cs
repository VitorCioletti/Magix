namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System;
    using Domain;
    using Domain.Interface;
    using Domain.Interface.Element;
    using Element;
    using Result;
    using Service.Interface;

    public class SelectingElementToCastState : BaseState
    {
        private readonly IWizard _wizard;
        private IElement _selectedElement;

        public SelectingElementToCastState(IWizard wizard)
        {
            _wizard = wizard;
        }

        public override void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {
            base.Initialize(stateMachineManager, boardController, matchService);

            BoardController.EnableElementMenuBar(true);
        }

        public override void OnGotBackOnTop(BaseStateResult stateResult)
        {
            switch (stateResult)
            {
                case SelectedTilesResult selectedTilesResult:
                    if (selectedTilesResult.Cancelled)
                    {
                        BoardController.EnableElementMenuBar(true);

                        return;
                    }

                    StateMachineManager.Swap(
                        new CastingElementState(_wizard, _selectedElement, selectedTilesResult.Tiles));

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stateResult));
            }

        }

        public override BaseStateResult Cleanup()
        {
            BoardController.EnableElementMenuBar(false);

            return null;
        }

        public override void OnClickElement(ElementButtonController elementButtonController)
        {
            _selectedElement = elementButtonController.Element;

            StateMachineManager.Push(new SelectingTilesIndividuallyState(_wizard, WizardActionType.CastElement));

            BoardController.EnableElementMenuBar(false);
        }
    }
}
