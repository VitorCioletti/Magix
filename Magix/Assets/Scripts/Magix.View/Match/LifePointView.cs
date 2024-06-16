namespace Magix.View.Match
{
    using UnityEngine;
    using UnityEngine.UI;

    public class LifePointView : MonoBehaviour
    {
        [field: SerializeField]
        private Image _background { get; set; }

        public void Enable(bool enable)
        {
            _background.color = enable ? Color.red : Color.black;
        }
    }
}
