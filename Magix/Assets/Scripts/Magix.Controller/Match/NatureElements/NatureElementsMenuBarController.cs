namespace Magix.Controller.Match.NatureElements
{
    using System.Collections.Generic;
    using Domain.Interface.NatureElements;
    using UnityEngine;
    using UnityEngine.Events;

    public class NatureElementsMenuBarController : MonoBehaviour
    {
        [field: SerializeField]
        private NatureElementButtonController _natureElementButtonControllerPrefab { get; set; }

        [field: SerializeField]
        private Transform _containerTransform { get; set; }

        public void Initialize(
            List<INatureElement> natureElements,
            UnityAction<NatureElementButtonController> onClickElement)
        {
            foreach (INatureElement natureElement in natureElements)
            {
                NatureElementButtonController natureElementButtonController =
                    Instantiate(_natureElementButtonControllerPrefab, _containerTransform);

                natureElementButtonController.Initialize(natureElement, onClickElement);
            }
        }
    }
}
