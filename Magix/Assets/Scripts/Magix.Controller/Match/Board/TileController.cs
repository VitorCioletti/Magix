namespace Magix.Controller.Match.Board
{
    using System;
    using Domain.Interface.Board;
    using Domain.Interface.NatureElements;
    using TMPro;
    using UnityEngine;
    using View.Match.Board;

    public class TileController : MonoBehaviour
    {
        public ITile Tile { get; set; }

        [field: SerializeField]
        private Collider2D _collider2D { get; set; }

        [field: SerializeField]
        private TileView _view { get; set; }

        [field: SerializeField]
        private TextMeshPro _natureElementText { get; set; }

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
            INatureElement natureElement = Tile.NatureElement;

            switch (natureElement)
            {
                case IEletric:
                    _natureElementText.text = "Eletric";
                    break;

                case IFire:
                    _natureElementText.text = "Fire";
                    break;

                case INatural:
                    _natureElementText.text = "Natural";
                    break;

                case ISmoke:
                    _natureElementText.text = "Smoke";
                    break;

                case IWater:
                    _natureElementText.text = "Water";
                    break;

                case IWind:
                    _natureElementText.text = "Wind";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(natureElement));
            }
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
