namespace Magix.Controller.Match.Board
{
    using Domain.Interface.NatureElements;
    using UnityEngine;
    using View.Match.Board;

    public class NatureElementController : MonoBehaviour
    {
        [field: SerializeField]
        private NatureElementView _view { get; set; }

        public void UpdateElement(INatureElement element)
        {
            _view.UpdateElement(element);
        }
    }
}
