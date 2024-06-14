namespace Magix.View.Match.Board
{
    using System.Collections.Generic;
    using UnityEngine;

    public class TileView : MonoBehaviour
    {
        [field: SerializeField]
        private SpriteRenderer _tileSpriteRenderer { get; set; }

        [field: SerializeField]
        private Color _previewColor { get; set; }

        [field: SerializeField]
        private Color _selectedColor { get; set; }

        [field: SerializeField]
        private Color _normalColor { get; set; }

        [field: SerializeField]
        private List<Sprite> _tileSprites { get; set; }

        public void Initialize()
        {
            int spriteIndex = Random.Range(0, _tileSprites.Count);

            Sprite sprite = _tileSprites[spriteIndex];

            _tileSpriteRenderer.sprite = sprite;
        }

        public void SetToSelected()
        {
            _tileSpriteRenderer.color = _selectedColor;

        }

        public void SetToPreview()
        {
            _tileSpriteRenderer.color = _previewColor;
        }

        public void SetToNormal()
        {
            _tileSpriteRenderer.color = _normalColor;
        }
    }
}
