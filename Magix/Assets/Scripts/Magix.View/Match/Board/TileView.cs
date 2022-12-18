namespace Magix.View.Match.Board
{
    using System.Collections.Generic;
    using UnityEngine;

    public class TileView : MonoBehaviour
    {
        [field: SerializeField]
        private SpriteRenderer _tileSpriteRenderer { get; set; }

        [field: SerializeField]
        private List<Sprite> _tileSprites { get; set; }

        [field: SerializeField]
        private float _highlightAlpha { get; set; }

        public void Initialize()
        {
            int spriteIndex = Random.Range(0, _tileSprites.Count);

            Sprite sprite = _tileSprites[spriteIndex];

            _tileSpriteRenderer.sprite = sprite;
        }

        public void Select()
        {
            Color color = _tileSpriteRenderer.color;

            _tileSpriteRenderer.color = new Color(
                color.r,
                color.g,
                color.b,
                _highlightAlpha);
        }

        public void Deselect()
        {
            Color color = _tileSpriteRenderer.color;

            _tileSpriteRenderer.color = new Color(
                color.r,
                color.g,
                color.b,
                1);
        }
    }
}
