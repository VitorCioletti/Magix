namespace Magix.Controller.Match.StateMachine.States
{
    using Service.Interface;

    public abstract class BaseState
    {
        protected StateMachineManager StateMachineManager { get; set; }

        protected IMatchService MatchService { get; set; }

        protected BoardController BoardController { get; set; }

        public virtual void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {
            BoardController = boardController;
            StateMachineManager = stateMachineManager;
            MatchService = matchService;
        }

        public virtual void Cleanup()
        {
        }

        public virtual void OnClickTile(TileController tileController)
        {
        }

        public virtual void OnEnterMouse(TileController tileController)
        {
        }

        public virtual void OnExitMouse(TileController tileController)
        {
        }
    }
}
