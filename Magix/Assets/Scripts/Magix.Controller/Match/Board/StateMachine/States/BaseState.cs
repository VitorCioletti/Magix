namespace Magix.Controller.Match.Board.StateMachine.States
{
    using NatureElements;
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

        public virtual void OnClickCastNatureElement()
        {
        }

        public virtual void OnClickNatureElementButton(NatureElementButtonController natureElementButtonController)
        {
        }

        protected void Pop()
        {
            StateMachineManager.Pop(this);
        }
    }
}
