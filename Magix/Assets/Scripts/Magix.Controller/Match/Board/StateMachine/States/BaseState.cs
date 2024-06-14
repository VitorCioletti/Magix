namespace Magix.Controller.Match.Board.StateMachine.States
{
    using Domain;
    using NatureElements;
    using Result;
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

            BoardController.EnableCancelButton(true);
        }

        public virtual void OnGotBackOnTop(BaseStateResult stateResult)
        {

        }

        public virtual BaseStateResult Cleanup()
        {
            return null;
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

        public virtual void OnClickWizardAction(WizardActionType actionType)
        {

        }

        public virtual void OnClickNatureElement(NatureElementButtonController natureElementController)
        {

        }

        public virtual void OnClickExecute()
        {

        }

        public void OnClickCancel()
        {
            Pop();
        }

        protected void Pop()
        {
            StateMachineManager.Pop(this);
        }
    }
}
