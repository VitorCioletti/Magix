namespace Magix.Controller.Match.StateMachine.States
{
    using System.Collections.Generic;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Domain.Interface.Board.Result;
    using Domain.Interface.NatureElements;
    using Service.Interface;
    using UnityEngine;

    public class ApplyingNatureElementState : BaseState
    {
        private readonly IWizard _wizard;

        private readonly List<ITile> _tiles;

        private readonly INatureElement _natureElement;

        public ApplyingNatureElementState(IWizard wizard, INatureElement natureElement, List<ITile> tiles)
        {
            _natureElement = natureElement;
            _wizard = wizard;
            _tiles = tiles;
        }

        public override void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {
            base.Initialize(stateMachineManager, boardController, matchService);

            IApplyNatureElementResult applyNatureElementResult =
                matchService.Board.ApplyNatureElement(_wizard, _natureElement, _tiles);

            if (applyNatureElementResult.Success)
                BoardController.Move(_wizard, applyNatureElementResult.Tiles);

            else
                Debug.LogError($"\"{applyNatureElementResult.ErrorId}\".");
        }
    }
}
