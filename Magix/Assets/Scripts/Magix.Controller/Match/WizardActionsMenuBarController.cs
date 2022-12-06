namespace Magix.Controller.Match
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class WizardActionsMenuBarController : MonoBehaviour
    {
        [field: SerializeField]
        private Button _moveButton { get; set; } = default;

        [field: SerializeField]
        private Button _applyNatureElementButton { get; set; } = default;

        public void Initialize(UnityAction onClickMoveButton, UnityAction onClickApplyNatureElementButton)
        {
            _moveButton.onClick.RemoveAllListeners();
            _moveButton.onClick.AddListener(onClickMoveButton);

            _applyNatureElementButton.onClick.RemoveAllListeners();
            _applyNatureElementButton.onClick.AddListener(onClickApplyNatureElementButton);
        }
    }
}
