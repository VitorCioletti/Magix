namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System;
    using Domain.Interface;
    using Result;
    using Service.Interface;

    public class SelectingTilesToMoveState : BaseState
    {
        private readonly IWizard _wizard;

        public SelectingTilesToMoveState(IWizard wizard)
        {
            _wizard = wizard;
        }

        public override void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {
            base.Initialize(stateMachineManager, boardController, matchService);

            StateMachineManager.Push(new SelectingTilesState(_wizard));
        }

        public override void OnGotBackOnTop(BaseStateResult stateResult)
        {
            switch (stateResult)
            {
                case SelectedTilesResult selectedTilesResult:
                    StateMachineManager.Swap(new MovingWizardToTargetState(_wizard, selectedTilesResult.Tiles));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stateResult));
            }
        }
    }
}
