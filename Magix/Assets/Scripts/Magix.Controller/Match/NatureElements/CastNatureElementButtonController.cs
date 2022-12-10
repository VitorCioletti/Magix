namespace Magix.Controller.Match.NatureElements
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class CastNatureElementButtonController : MonoBehaviour
    {
        [field: SerializeField]
        private Button _button { get; set; }

        public void Initialize(UnityAction onClickButton)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(onClickButton);
        }
    }
}
