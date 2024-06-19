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
        private Button _applyElementButton { get; set; }

        [field: SerializeField]
        private Button _attackButton { get; set; }

        public void Initialize(
            UnityAction onClickMoveAction,
            UnityAction onClickAttackAction,
            UnityAction onClickApplyElementButton)
        {
            _moveButton.onClick.RemoveAllListeners();
            _moveButton.onClick.AddListener(onClickMoveAction);

            _applyElementButton.onClick.RemoveAllListeners();
            _applyElementButton.onClick.AddListener(onClickApplyElementButton);

            _attackButton.onClick.RemoveAllListeners();
            _attackButton.onClick.AddListener(onClickAttackAction);
        }

        public void EnableAttackButton(bool enable)
        {
            _attackButton.interactable = enable;
        }
    }
}
