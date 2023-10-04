namespace Magix.Controller.Match.Board
{
    using System;
    using Domain.Interface.Board;
    using Domain.Interface.NatureElements;
    using TMPro;
    using UnityEngine;
    using View.Match.Board;

    public class NatureElementController : MonoBehaviour
    {
        public INatureElement NatureElement { get; private set; }

        [field: SerializeField]
        private TextMeshProUGUI _placeHolderNatureElementName { get; set; }

        [field: SerializeField]
        private RuntimeAnimatorController _fireRuntimeAnimator { get; set; }

        [field: SerializeField]
        private NatureElementView _view { get; set; }

        public void Initialize(INatureElement natureElement)
        {
            NatureElement = natureElement;

            _view.Initialize();
        }

        public void UpdateNatureElement(ITile tile)
        {
            // NatureElement = tile.NatureElement;

            switch (NatureElement)
            {
                case IEletric:
                    _placeHolderNatureElementName.text = "Eletric";
                    break;

                case IFire:
                    _view.UpdateNatureElement(_fireRuntimeAnimator);
                    break;

                case INatural:
                    _placeHolderNatureElementName.text = string.Empty;
                    break;

                case ISmoke:
                    _placeHolderNatureElementName.text = "Smoke";
                    break;

                case IWater:
                    _placeHolderNatureElementName.text = "Water";
                    break;

                case IWind:
                    _placeHolderNatureElementName.text = "Wind";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(NatureElement));
            }
        }
    }
}
