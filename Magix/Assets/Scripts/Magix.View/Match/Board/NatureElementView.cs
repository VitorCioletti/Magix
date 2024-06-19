namespace Magix.View.Match.Board
{
    using System;
    using Domain.Interface.NatureElements;
    using UnityEngine;

    public class NatureElementView : MonoBehaviour
    {
        [field: SerializeField]
        private SpriteRenderer _spriteRenderer { get; set; }

        [field: SerializeField]
        private Sprite _electric { get; set; }

        [field: SerializeField]
        private Sprite _fire { get; set; }

        [field: SerializeField]
        private Sprite _smoke { get; set; }

        [field: SerializeField]
        private Sprite _water { get; set; }

        public void UpdateElement(INatureElement element)
        {
            Sprite sprite = element switch
            {
                IElectric => _electric,
                IFire => _fire,
                INatural => null,
                ISmoke => _smoke,
                IWater => _water,
                IWind => null,
                _ => throw new ArgumentOutOfRangeException(nameof(element))
            };

            _spriteRenderer.sprite = sprite;
        }
    }
}
