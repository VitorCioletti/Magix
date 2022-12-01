namespace Magix.Controller.Match
{
    using System;
    using UnityEngine;

    public class TileController : MonoBehaviour
    {
        [field: SerializeField]
        public SpriteRenderer SpriteRenderer { get; private set; } = default;

        [field: SerializeField]
        private Collider2D _collider2D { get; set; } = default;

        [field: SerializeField]
        private float _highlightAlpha { get; set; } = default;

        private Action<TileController> _onMouseEntered;

        private Action<TileController> _onMouseExited;

        private Action<TileController> _onTileClicked;

        public void Init(
            Action<TileController> onMouseEntered,
            Action<TileController> onMouseExited,
            Action<TileController> onTileClicked)
        {
            _onTileClicked = onTileClicked;
            _onMouseEntered = onMouseEntered;
            _onMouseExited = onMouseExited;
        }

        public void Select()
        {
            Color color = SpriteRenderer.color;

            SpriteRenderer.color = new Color(color.r, color.g, color.b, _highlightAlpha);
        }

        public void Deselect()
        {
            Color color = SpriteRenderer.color;

            SpriteRenderer.color = new Color(color.r, color.g, color.b, 1);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && Camera.main is not null)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (_collider2D.OverlapPoint(mousePosition))
                    _onTileClicked?.Invoke(this);
            }
        }

        private void OnMouseEnter()
        {
            _onMouseEntered?.Invoke(this);
        }

        private void OnMouseExit()
        {
            _onMouseExited?.Invoke(this);
        }
    }
}
