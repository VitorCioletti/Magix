namespace Magix.Controller.Match
{
    using System;
    using Domain.Interface.Board;
    using UnityEngine;

    public class GridController : MonoBehaviour
    {
        [field: SerializeField]
        private TileController _tilePrefab { get; set; } = default;

        public TileController[,] Tiles { get; private set; }

        public void Init(
            ITile[,] tiles,
            Action<TileController> onMouseEntered,
            Action<TileController> onMouseExited,
            Action<TileController> onTileClicked)
        {
            _createTiles(
                tiles,
                onMouseEntered,
                onMouseExited,
                onTileClicked);
        }

        private void _createTiles(
            ITile[,] tiles,
            Action<TileController> onMouseEntered,
            Action<TileController> onMouseExited,
            Action<TileController> onTileClicked)
        {
            int gridLines = tiles.GetUpperBound(0) + 1;
            int gridColumns = tiles.GetUpperBound(1) + 1;

            Tiles = new TileController[gridLines, gridColumns];

            float tileSize = 1f;

            int tileSortingLayer = gridColumns;

            for (int line = 0; line < gridLines; line++)
            {
                for (int column = 0; column < gridColumns; column++)
                {
                    ITile tile = tiles[line, column];

                    int tileX = tile.Position.X;
                    int tileY = tile.Position.Y;

                    float positionX = (tileX * tileSize + tileY * tileSize) / 2f;
                    float positionY = (tileX * tileSize - tileY * tileSize) / 4f;

                    var tilePosition = new Vector2(positionX, positionY);

                    TileController tileController = Instantiate(
                        _tilePrefab,
                        tilePosition,
                        Quaternion.identity,
                        transform);

                    tileController.Init(
                        tile,
                        onMouseEntered,
                        onMouseExited,
                        onTileClicked);

                    tileController.SpriteRenderer.sortingOrder = tileSortingLayer;
                    tileController.gameObject.name = $"Tile:{tileX},{tileY}";

                    Tiles[tileX, tileY] = tileController;
                }

                tileSortingLayer--;
            }
        }
    }
}
