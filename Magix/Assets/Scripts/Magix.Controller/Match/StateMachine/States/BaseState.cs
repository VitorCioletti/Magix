namespace Magix.Controller.Match.StateMachine.States
{
    using Service.Interface;

    public abstract class BaseState
    {
        protected StateMachineManager StateMachineManager { get; private set; }

        protected IMatchService MatchService { get; private set; }

        protected BoardController BoardController { get; private set; }

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

        public virtual void OnClickActionMove()
        {
        }

        public virtual void OnClickActionApplyNatureElement()
        {
        }

        protected void Pop()
        {
            StateMachineManager.Pop(this);
        }
    }
}
