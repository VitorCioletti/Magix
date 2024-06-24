namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System.Collections.Generic;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Domain.Interface.Board.Result;
    using Service.Interface;

    public class MovingWizardState : BaseState
    {
        private readonly IWizard _wizard;

        private readonly IList<ITile> _tiles;

        public MovingWizardState(IWizard wizard, IList<ITile> tiles)
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

            IMovementResult result = await BoardController.MoveAsync(_wizard, _tiles);

            if (result.GameEnded)
            {
                StateMachineManager.Swap(new EndGameState(result.Winner));

                return;
            }

            Pop();
        }
    }
}
