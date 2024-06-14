namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System;
    using Domain.Interface;
    using Result;
    using Service.Interface;

    public class SelectingTilesToAttackState : BaseState
    {
        private readonly IWizard _wizard;

        public SelectingTilesToAttackState(IWizard wizard)
        {
            _wizard = wizard;
        }

        public override void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {
            base.Initialize(stateMachineManager, boardController, matchService);

            StateMachineManager.Push(new SelectingTilesPathState(_wizard));
        }

        public override void OnGotBackOnTop(BaseStateResult stateResult)
        {
            if (stateResult is null)
            {
                Pop();

                return;
            }

            switch (stateResult)
            {
                case SelectedTilesResult selectedTilesResult:
                    StateMachineManager.Swap(new AttackingTargetState(_wizard, selectedTilesResult.Tiles));

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stateResult));
            }
        }
    }
}
