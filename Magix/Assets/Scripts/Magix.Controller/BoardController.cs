namespace Magix.Controller
{
    using UnityEngine;
    using UnityEngine.Tilemaps;

    public class BoardController : MonoBehaviour
    {
        [field: SerializeField]
        private Tilemap _tilemap { get; set; } = default;

        [field: SerializeField]
        private TileController _tilePrefab { get; set; } = default;

        [field: SerializeField]
        private TileController[,] _tiles { get; set; }

        private void Start()
        {
            Init(10);
        }

        public void Init(int size)
        {
            _tiles = new TileController[size, size];

            var position = new Vector3Int(0, 0);

            TileController tile = Instantiate(_tilePrefab);

            _tiles[0, 0] = tile;

            _tilemap.SetTile(position, tile);
        }
    }
}
