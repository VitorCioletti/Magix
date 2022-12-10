namespace Magix.Controller.Match.NatureElements
{
    using System;
    using Domain.Interface.NatureElements;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class NatureElementButtonController : MonoBehaviour
    {
        public INatureElement NatureElement { get; private set; }

        [field: SerializeField]
        private TextMeshProUGUI _name { get; set; }

        [field: SerializeField]
        private Button _button { get; set; }

        public void Initialize(INatureElement natureElement, UnityAction<NatureElementButtonController> onClick)
        {
            NatureElement = natureElement;

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => onClick(this));

            _setName(natureElement);
        }

        private void _setName(INatureElement natureElement)
        {
            switch (natureElement)
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
                    throw new InvalidOperationException($"Element \"{natureElement.GetType().Name}\" not supported");
            }
        }
    }
}
