namespace Magix.Controller.Match.Board
{
    using System;
    using Domain.Interface.Board;
    using TMPro;
    using UnityEngine;
    using View.Match.Board;

    public class TileController : MonoBehaviour
    {
        public ITile Tile { get; set; }

        [field: SerializeField]
        private Collider2D _collider2D { get; set; }

        [field: SerializeField]
        private NatureElementController _natureElementController { get; set; }

        [field: SerializeField]
        private TileView _view { get; set; }

        private Action<TileController> _onMouseEntered;

        private Action<TileController> _onMouseExited;

        private Action<TileController> _onTileClicked;

        private Camera _mainCamera;

        public void Initialize(
            ITile tile,
            Action<TileController> onMouseEntered,
            Action<TileController> onMouseExited,
            Action<TileController> onTileClicked)
        {
            Tile = tile;

            _mainCamera = Camera.main;

            _onTileClicked = onTileClicked;
            _onMouseEntered = onMouseEntered;
            _onMouseExited = onMouseExited;

            _view.Initialize();
            // _natureElementController.Initialize(Tile.NatureElement);
        }

        public void Select()
        {
            _view.Select();
        }

        public void Deselect()
        {
            _view.Deselect();
        }

        public void UpdateNatureElement()
        {
            _natureElementController.UpdateNatureElement(Tile);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _mainCamera is not null)
            {
                Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

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
