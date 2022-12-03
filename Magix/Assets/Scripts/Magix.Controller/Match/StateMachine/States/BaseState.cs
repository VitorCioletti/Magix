namespace Magix.Controller.Match.StateMachine.States
{
    public abstract class BaseState
    {
        protected StateMachineManager StateMachineManager { get; set; }

        public void Initialize(StateMachineManager stateMachineManager)
        {
            StateMachineManager = stateMachineManager;
        }

        public virtual void OnClickTile(TileController tileController)
        {
        }

        public virtual void OnMouseEntered(TileController tileController)
        {
        }

        public virtual void OnMouseExited(TileController tileController)
        {
        }
    }
}
