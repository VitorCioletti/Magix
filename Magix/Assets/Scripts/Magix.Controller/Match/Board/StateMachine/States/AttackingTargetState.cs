namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Domain.Interface.Board.Result;
    using Service.Interface;

    public class AttackingTargetState : BaseState
    {
        private readonly IWizard _wizard;

        private readonly IList<ITile> _tiles;

        public AttackingTargetState(IWizard wizard, IList<ITile> tiles)
        {
            _wizard = wizard;
            _tiles = tiles;
        }

        public override async void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {
            base.Initialize(stateMachineManager, boardController, matchService);

            BoardController.EnableCancelButton(false);

            List<IAttackResult> results = await BoardController.AttackAsync(_wizard, _tiles);

            IAttackResult endResult = results.FirstOrDefault(r => r.GameEnded);

            if (endResult is not null)
            {
                StateMachineManager.Swap(new EndGameState(endResult.Winner));

                return;
            }

            Pop();
        }
    }
}
