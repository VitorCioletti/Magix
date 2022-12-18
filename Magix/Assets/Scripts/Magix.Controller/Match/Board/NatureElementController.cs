namespace Magix.Controller.Match.Board
{
    using System;
    using Domain.Interface.Board;
    using Domain.Interface.NatureElements;
    using UnityEngine;
    using View.Match.Board;

    public class NatureElementController : MonoBehaviour
    {
        [field: SerializeField]
        private RuntimeAnimatorController _fireRuntimeAnimator { get; set; }

        [field: SerializeField]
        private NatureElementView _view { get; set; }

        public void Initialize()
        {
            _view.Initialize();
        }

        public void UpdateNatureElement(ITile tile)
        {
            INatureElement natureElement = tile.NatureElement;

            switch (natureElement)
            {
                case IEletric:
                    _view.UpdateNatureElement(null);
                    break;

                case IFire:
                    _view.UpdateNatureElement(_fireRuntimeAnimator);
                    break;

                case INatural:
                    _view.UpdateNatureElement(null);
                    break;

                case ISmoke:
                    _view.UpdateNatureElement(null);
                    break;

                case IWater:
                    _view.UpdateNatureElement(null);
                    break;

                case IWind:
                    _view.UpdateNatureElement(null);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(natureElement));
            }
        }
    }
}
