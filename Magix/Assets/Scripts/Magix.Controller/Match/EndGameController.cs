namespace Magix.Controller.Match
{
    using Domain.Interface;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class EndGameController : MonoBehaviour
    {
        [field: SerializeField]
        private Button _restartButton { get; set; }

        [field: SerializeField]
        private TextMeshProUGUI _playerWonText { get; set; }

        public void Init(UnityAction onClickRestart)
        {
            _restartButton.onClick.RemoveAllListeners();
            _restartButton.onClick.AddListener(onClickRestart);
        }

        public void SetWinner(IPlayer player)
        {
            _playerWonText.text = string.Format(_playerWonText.text, player.Number);
        }
    }
}
