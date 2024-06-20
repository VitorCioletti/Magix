namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System;
    using Domain;
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

            StateMachineManager.Push(new SelectingTilesIndividuallyState(_wizard, WizardActionType.Attack));
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
                    if (selectedTilesResult.Cancelled)
                    {
                        Pop();

                        return;
                    }

                    StateMachineManager.Swap(new AttackingTargetState(_wizard, selectedTilesResult.Tiles));

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stateResult));
            }
        }
    }
}
