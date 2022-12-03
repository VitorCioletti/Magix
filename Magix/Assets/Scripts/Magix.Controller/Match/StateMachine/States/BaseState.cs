namespace Magix.Controller.Match.StateMachine.States
{
    public abstract class BaseState
    {
        protected StateMachineManager StateMachineManager { get; set; }

        public virtual void Initialize(StateMachineManager stateMachineManager)
        {
            StateMachineManager = stateMachineManager;
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
