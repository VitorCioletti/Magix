namespace Magix.Controller.Match.Wizard
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class WizardActionsMenuBarController : MonoBehaviour
    {
        [field: SerializeField]
        private Button _moveButton { get; set; }

        [field: SerializeField]
        private Button _applyNatureElementButton { get; set; }

        [field: SerializeField]
        private Button _attackButton { get; set; }

        public void Initialize(
            UnityAction onClickMoveAction,
            UnityAction onClickMoveButton,
            UnityAction onClickApplyNatureElementButton)
        {
            _moveButton.onClick.RemoveAllListeners();
            _moveButton.onClick.AddListener(onClickMoveButton);

            _applyNatureElementButton.onClick.RemoveAllListeners();
            _applyNatureElementButton.onClick.AddListener(onClickApplyNatureElementButton);

            _attackButton.onClick.RemoveAllListeners();
            _attackButton.onClick.AddListener(onClickApplyNatureElementButton);
        }
    }
}
