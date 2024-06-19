namespace Magix.Controller.Match.Element
{
    using System.Collections.Generic;
    using Domain.Interface.Element;
    using UnityEngine;
    using UnityEngine.Events;

    public class ElementMenuBarController : MonoBehaviour
    {
        [field: SerializeField]
        private ElementButtonController ElementButtonControllerPrefab { get; set; }

        [field: SerializeField]
        private Transform _containerTransform { get; set; }

        public void Initialize(List<IElement> natureElements, UnityAction<ElementButtonController> onClickElement)
        {
            foreach (IElement natureElement in natureElements)
            {
                ElementButtonController elementButtonController =
                    Instantiate(ElementButtonControllerPrefab, _containerTransform);

                elementButtonController.Initialize(natureElement, onClickElement);
            }
        }
    }
}
