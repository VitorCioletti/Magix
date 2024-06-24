namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System.Collections.Generic;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Domain.Interface.Board.Result;
    using Domain.Interface.Element;
    using Service.Interface;

    public class CastingElementState : BaseState
    {
        private readonly IWizard _wizard;

        private readonly IList<ITile> _tiles;

        private readonly IElement _element;

        public CastingElementState(IWizard wizard, IElement element, IList<ITile> tiles)
        {
            _element = element;
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

            ICastResult result = await BoardController.CastElementAsync(_wizard, _element, _tiles);

            if (result.GameEnded)
            {
                StateMachineManager.Swap(new EndGameState(result.Winner));

                return;
            }

            Pop();
        }
    }
}
