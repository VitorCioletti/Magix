namespace Magix.Controller.Match.Board
{
    using Domain.Interface.Element;
    using UnityEngine;
    using View.Match.Board;

    public class ElementController : MonoBehaviour
    {
        [field: SerializeField]
        private ElementView _view { get; set; }

        public void UpdateElement(IElement element)
        {
            _view.UpdateElement(element);
        }
    }
}
