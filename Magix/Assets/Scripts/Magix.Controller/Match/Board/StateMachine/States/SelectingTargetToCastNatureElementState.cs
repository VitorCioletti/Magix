namespace Magix.Controller.Match.Board.StateMachine.States
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Domain.Interface.NatureElements;
    using Service.Interface;

    public class SelectingTargetToCastNatureElementState : BaseState
    {
        private readonly IWizard _wizard;

        private readonly INatureElement _natureElement;

        private List<TileController> _selectedTilesController;

        public SelectingTargetToCastNatureElementState(IWizard wizard, INatureElement natureElement)
        {
            _natureElement = natureElement;
            _wizard = wizard;
        }

        public override void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {
            base.Initialize(stateMachineManager, boardController, matchService);

            _selectedTilesController = new List<TileController>();

            BoardController.EnableCastNatureElementButton(true);
        }

        public override void Cleanup()
        {
            foreach (TileController tileController in _selectedTilesController)
                tileController.Deselect();

            _selectedTilesController.Clear();

            BoardController.EnableCastNatureElementButton(false);
        }

        public override void OnClickTile(TileController tileController)
        {
            if (_selectedTilesController.Contains(tileController))
            {
                tileController.Deselect();
                _selectedTilesController.Remove(tileController);
            }
            else if (_wizard.RemainingActions > _selectedTilesController.Count)
            {
                tileController.Select();
                _selectedTilesController.Add(tileController);
            }
        }

        public override void OnClickCastNatureElement()
        {
            if (_selectedTilesController.Count == 0)
                return;

            List<ITile> selectedTiles = _selectedTilesController.Select(t => t.Tile).ToList();

            StateMachineManager.Swap(new ApplyingNatureElementState(_wizard, _natureElement, selectedTiles));
        }
    }
}
