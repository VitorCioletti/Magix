namespace Magix.Controller.Match.Wizard
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Board;
    using Domain.Interface;
    using UnityEngine;
    using View.Match.Wizard;

    public class WizardController : MonoBehaviour
    {
        [field: SerializeField]
        private WizardView _view { get; set; }

        public IWizard Wizard { get; private set; }

        public void Initialize(IWizard wizard, TileController tileToSpawnWizard)
        {
            Wizard = wizard;

            _view.Initialize(tileToSpawnWizard.transform);
            _view.AnimateIdle();
        }

        public async Task MoveAsync(List<TileController> tilesController)
        {
            List<Transform> tilesTransforms = tilesController.Select(t => t.transform).ToList();

            await _view.AnimateMoveAsync(tilesTransforms);
        }

        public async Task CastAsync()
        {
            await _view.AnimateCastAsync();
        }
    }
}
