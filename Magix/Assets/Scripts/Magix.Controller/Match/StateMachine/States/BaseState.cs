namespace Magix.Controller.Match.StateMachine.States
{
    public abstract class BaseState
    {
        public void Initialize()
        {
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
