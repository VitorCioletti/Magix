namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Service.Interface;

    public class MovingWizardToTargetState : BaseState
    {
        private readonly IWizard _wizard;

        private readonly List<ITile> _tiles;

        public MovingWizardToTargetState(IWizard wizard, List<ITile> tiles)
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

            await BoardController.MoveAsync(_wizard, _tiles);

            Pop();
        }
    }
}
