namespace Magix.Controller.Match.Board
{
    using System.Collections.Generic;
    using Domain.Interface.Board;
    using Domain.Interface.NatureElements;
    using TMPro;
    using UnityEngine;
    using View.Match.Board;

    public class NatureElementController : MonoBehaviour
    {
        private IList<INatureElement> NatureElements { get; set; }

        [field: SerializeField]
        private TextMeshPro _placeHolderNatureElementName { get; set; }

        [field: SerializeField]
        private RuntimeAnimatorController _fireRuntimeAnimator { get; set; }

        [field: SerializeField]
        private NatureElementView _view { get; set; }

        public void Initialize(IList<INatureElement> elements)
        {
            NatureElements = elements;

            // _view.Initialize();
        }

        public void UpdateNatureElements(ITile tile)
        {
            NatureElements = tile.NatureElements;

            _placeHolderNatureElementName.text = string.Empty;

            foreach (INatureElement natureElement in NatureElements)
            {
                if (natureElement is INatural)
                    return;

                UpdateNatureElement(natureElement);
            }
        }

        private void UpdateNatureElement(INatureElement natureElement)
        {
            _placeHolderNatureElementName.text += $" {natureElement.GetType().Name}";
        }
    }
}
