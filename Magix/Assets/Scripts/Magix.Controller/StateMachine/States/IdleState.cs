namespace Magix.Controller.StateMachine.States
{
    using UnityEngine;

    public class IdleState : BaseState
    {
        public override void OnClickTile(TileController tileController)
        {
            Debug.Log($"{tileController.name} clicked.");
        }

        public override void OnMouseEntered(TileController tileController)
        {
            tileController.Select();
        }

        public override void OnMouseExited(TileController tileController)
        {
            tileController.Deselect();
        }
    }
}
