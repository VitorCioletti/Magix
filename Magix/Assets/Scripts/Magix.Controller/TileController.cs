namespace Magix.Controller
{
    using UnityEngine;
    using UnityEngine.Tilemaps;

    public class TileController : Tile
    {
        [field: SerializeField]
        private Sprite _tileSprite { get; set; }
    }
}
