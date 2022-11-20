namespace Magix.Controller
{
    using UnityEngine;

    public class GridController : MonoBehaviour
    {
        [field: SerializeField]
        private TileController _tilePrefab { get; set; } = default;

        public TileController[,] Tiles { get; private set; }

        private const int _size = 10;

        public void Init()
        {
            _createTiles();
        }

        private void _createTiles()
        {
            Tiles = new TileController[_size, _size];

            float tileGap = 1f;

            int tileOrderInLayer = _size;

            for (int x = 0; x < _size; x++)
            {
                for (int y = 0; y < _size; y++)
                {
                    float positionX = (x * tileGap + y * tileGap) / 2f;
                    float positionY = (x * tileGap - y * tileGap) / 4f;

                    var tilePosition = new Vector2(positionX, positionY);

                    TileController tile = Instantiate(_tilePrefab, tilePosition, Quaternion.identity, transform);

                    tile.SpriteRenderer.sortingOrder = tileOrderInLayer;
                    tile.gameObject.name = $"Tile:{x},{y}";

                    Tiles[x, y] = tile;
                }

                tileOrderInLayer--;
            }

            TileController firstTile = Tiles[0, 0];
            TileController lastTile = Tiles[_size - 1, _size - 1];

            Vector3 gridCenter = firstTile.transform.position - lastTile.transform.position;

            transform.position = gridCenter;
        }
    }
}
