namespace Magix.Controller.ScriptableObject.Match.Wizard
{
    using System;
    using Domain.Interface.Element;
    using UnityEngine;

    [CreateAssetMenu]
    public class ElementEffectsScriptableObject : ScriptableObject
    {
        [field: SerializeField]
        public Sprite OnFire { get; private set; }

        [field: SerializeField]
        public Sprite Wet { get; private set; }

        [field: SerializeField]
        public Sprite Blind { get; private set; }

        [field: SerializeField]
        public Sprite Shocked { get; private set; }

        public Sprite GetSprite(ElementEffect elementEffect)
        {
            return elementEffect switch
            {
                ElementEffect.OnFire => OnFire,
                ElementEffect.Wet => Wet,
                ElementEffect.Blind => Blind,
                ElementEffect.Shocked => Shocked,
                _ => throw new ArgumentOutOfRangeException(nameof(elementEffect), elementEffect, null)
            };
        }
    }
}
