namespace Magix.Controller
{
    using UnityEngine;

    public class TileController : MonoBehaviour
    {
        [field: SerializeField]
        public SpriteRenderer SpriteRenderer { get; private set; } = default;

        [field: SerializeField]
        private float _highlightAlpha { get; set; } = default;

        private void OnMouseEnter()
        {
            Debug.Log("Enter");
            Color color = SpriteRenderer.color;

            SpriteRenderer.color = new Color(color.r, color.g, color.b, _highlightAlpha);
        }

        private void OnMouseExit()
        {
            Debug.Log("Down");
            Color color = SpriteRenderer.color;

            SpriteRenderer.color = new Color(color.r, color.g, color.b, 1);
        }
    }
}
