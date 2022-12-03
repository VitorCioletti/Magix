namespace Magix.Controller.Match.StateMachine.States
{
    using DependencyInjection;
    using Domain.Interface;
    using Service.Interface;

    public class IdleState : BaseState
    {
        private readonly GridController _gridController;

        public IdleState(GridController gridController)
        {
            _gridController = gridController;
        }

        public override void OnClickTile(TileController tileController)
        {
            var matchService = Resolver.GetService<IMatchService>();

            IWizard wizard = matchService.Board.GetWizard(tileController.Tile);

            if (wizard != null)
            {
                var selectingTargetToMoveWizardState = new SelectingTargetToMoveWizardState(wizard, _gridController);

                StateMachineManager.Push(selectingTargetToMoveWizardState);
            }
        }

        public override void OnEnterMouse(TileController tileController)
        {
            tileController.Select();
        }

        public override void OnExitMouse(TileController tileController)
        {
            tileController.Deselect();
        }
    }
}
