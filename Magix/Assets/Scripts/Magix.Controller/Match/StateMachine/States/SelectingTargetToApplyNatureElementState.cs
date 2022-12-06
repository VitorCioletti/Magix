namespace Magix.Controller.Match.StateMachine.States
{
    using System.Collections.Generic;
    using Domain.Interface;
    using Service.Interface;

    public class SelectingTargetToApplyNatureElementState : BaseState
    {
        private readonly IWizard _wizard;

        private List<TileController> _selectedTiles;

        public SelectingTargetToApplyNatureElementState(IWizard wizard)
        {
            _wizard = wizard;
        }

        public override void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {
            base.Initialize(stateMachineManager, boardController, matchService);

            _selectedTiles = new List<TileController>();
        }

        public override void Cleanup()
        {
            foreach (TileController tileController in _selectedTiles)
                tileController.Deselect();

            _selectedTiles.Clear();
        }

        public override void OnClickTile(TileController tileController)
        {
            if (_selectedTiles.Contains(tileController))
            {
                tileController.Deselect();
                _selectedTiles.Remove(tileController);
            }
            else if (_wizard.HasRemainingActions())
            {
                tileController.Select();
                _selectedTiles.Add(tileController);
            }
        }
    }
}
