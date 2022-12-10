namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System.Collections.Generic;
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

        public override void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {
            base.Initialize(stateMachineManager, boardController, matchService);

            BoardController.Move(_wizard, _tiles);

            Pop();
        }
    }
}
