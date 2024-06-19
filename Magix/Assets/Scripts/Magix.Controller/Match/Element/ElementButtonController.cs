namespace Magix.Controller.Match.Element
{
    using System;
    using Domain.Interface.Element;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class ElementButtonController : MonoBehaviour
    {
        public IElement Element { get; private set; }

        [field: SerializeField]
        private TextMeshProUGUI _name { get; set; }

        [field: SerializeField]
        private Button _button { get; set; }

        public void Initialize(IElement element, UnityAction<ElementButtonController> onClick)
        {
            Element = element;

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => onClick(this));

            _setName(element);
        }

        private void _setName(IElement element)
        {
            switch (element)
            {
                case IFire:
                    _name.text = "Fire";
                    break;

                case IWater:
                    _name.text = "Water";
                    break;

                case IWind:
                    _name.text = "Wind";
                    break;

                default:
                    throw new InvalidOperationException($"Element \"{element.GetType().Name}\" not supported");
            }
        }
    }
}
