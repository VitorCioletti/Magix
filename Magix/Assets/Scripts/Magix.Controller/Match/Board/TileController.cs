namespace Magix.Controller.Match.Board
{
    using System;
    using System.Collections.Generic;
    using Domain.Interface.Board;
    using Domain.Interface.NatureElements;
    using UnityEngine;
    using View;
    using View.Match.Board;

    public class TileController : MonoBehaviour
    {
        public ITile Tile { get; set; }

        [field: SerializeField]
        private Collider2D _collider2D { get; set; }

        [field: SerializeField]
        private NatureElementController _elementPrefab { get; set; }

        [field: SerializeField]
        private WorldHorizontalLayoutGroup _worldHorizontalLayoutGroup { get; set; }

        [field: SerializeField]
        private List<NatureElementController> _elements { get; set; }

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
            _elements = new List<NatureElementController>();

            _onTileClicked = onTileClicked;
            _onMouseEntered = onMouseEntered;
            _onMouseExited = onMouseExited;

            _view.Initialize();

            UpdateElements();
        }

        public void SetToSelected()
        {
            _view.SetToSelected();
        }

        public void SetToPreview()
        {
            _view.SetToPreview();
        }

        public void SetToNormal()
        {
            _view.SetToNormal();
        }

        public void UpdateElements()
        {
            foreach (NatureElementController elementController in _elements)
            {
                Destroy(elementController.gameObject);
            }

            _elements.Clear();

            List<INatureElement> elements = Tile.NatureElements;

            foreach (INatureElement element in elements)
            {
                NatureElementController newElement = Instantiate(_elementPrefab, _worldHorizontalLayoutGroup.transform);

                newElement.UpdateElement(element);

                _elements.Add(newElement);
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
