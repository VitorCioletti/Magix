namespace Magix.Controller.Match.Wizard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Interface.Element;
    using ScriptableObject.Match.Wizard;
    using UnityEngine;
    using UnityEngine.UI;

    public class ElementEffectsBarController : MonoBehaviour
    {
        [field: SerializeField]
        private ElementEffectsScriptableObject _elementEffectsScriptableObject { get; set; }

        private List<GameObject> _effects { get; set; }

        private void Start()
        {
            _effects = new List<GameObject>();
        }

        public void UpdateEffects(ElementEffect elementEffect)
        {
            _effects.ForEach(Destroy);
            _effects.Clear();

            IEnumerable<ElementEffect> effects = Enum.GetValues(typeof(ElementEffect)).Cast<ElementEffect>();

            foreach (ElementEffect effect in effects)
            {
                if (elementEffect.HasFlag(effect))
                    CreateEffect(effect);
            }
        }

        private void CreateEffect(ElementEffect elementEffect)
        {
            var newEffectGameObject = new GameObject {transform = {parent = gameObject.transform}};

            var image = newEffectGameObject.AddComponent<Image>();
            Sprite effectSprite = _elementEffectsScriptableObject.GetSprite(elementEffect);

            image.sprite = effectSprite;

            _effects.Add(newEffectGameObject);
        }
    }
}
