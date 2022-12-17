namespace Magix.View.Match.Board
{
    using System.Collections.Generic;
    using UnityEngine;

    public class TileView : MonoBehaviour
    {
        [field: SerializeField]
        private SpriteRenderer _spriteRenderer { get; set; }

        [field: SerializeField]
        private List<Sprite> _sprites { get; set; }

        [field: SerializeField]
        private float _highlightAlpha { get; set; }

        public void Initialize()
        {
            int spriteIndex = Random.Range(0, _sprites.Count);

            Sprite sprite = _sprites[spriteIndex];

            _spriteRenderer.sprite = sprite;
        }

        public void Select()
        {
            Color color = _spriteRenderer.color;

            _spriteRenderer.color = new Color(
                color.r,
                color.g,
                color.b,
                _highlightAlpha);
        }

        public void Deselect()
        {
            Color color = _spriteRenderer.color;

            _spriteRenderer.color = new Color(
                color.r,
                color.g,
                color.b,
                1);
        }
    }
}
