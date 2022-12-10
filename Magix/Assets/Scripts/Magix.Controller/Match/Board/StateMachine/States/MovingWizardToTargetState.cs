namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System.Collections.Generic;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Domain.Interface.Board.Result;
    using Service.Interface;
    using UnityEngine;

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

            IMovementResult movementResult = MatchService.Board.Move(_wizard, _tiles);

            if (movementResult.Success)
                BoardController.Move(_wizard, movementResult.Moves);

            else
                Debug.LogError($"\"{movementResult.ErrorId}\".");

            Pop();
        }
    }
}
