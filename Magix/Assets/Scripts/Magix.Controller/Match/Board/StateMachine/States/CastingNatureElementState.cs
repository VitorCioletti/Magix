namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System.Collections.Generic;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Domain.Interface.NatureElements;
    using Service.Interface;

    public class CastingNatureElementState : BaseState
    {
        private readonly IWizard _wizard;

        private readonly IList<ITile> _tiles;

        private readonly INatureElement _natureElement;

        public CastingNatureElementState(IWizard wizard, INatureElement natureElement, IList<ITile> tiles)
        {
            _natureElement = natureElement;
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

            await BoardController.CastNatureElementAsync(_wizard, _natureElement, _tiles);

            Pop();
        }
    }
}
