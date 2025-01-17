namespace Magix.Controller.Match.Wizard
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Board;
    using Domain.Interface;
    using Domain.Interface.Board;
    using UnityEngine;
    using View.Match.Wizard;

    public class WizardController : MonoBehaviour
    {
        [field: SerializeField]
        private LifeBarController _lifeBarController { get; set; }

        [field: SerializeField]
        private ElementEffectsBarController _elementEffectsBarController { get; set; }

        [field: SerializeField]
        private WizardView _view { get; set; }

        public IWizard Wizard { get; private set; }

        public void Initialize(IWizard wizard, TileController tileToSpawnWizard, int player)
        {
            _lifeBarController.Initialize(wizard.LifePoints);

            Wizard = wizard;

            _view.Initialize(tileToSpawnWizard.transform, player);
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

        public async Task AttackAsync(ITile tile)
        {
            await _view.AnimateAttackAsync();
        }

        public async Task TakeDamageAsync(int effectResultDamageTaken)
        {
            await _view.AnimateTakeDamageAsync(effectResultDamageTaken);

            UpdateStats();
        }

        private void UpdateStats()
        {
            _lifeBarController.UpdateLife(Wizard.LifePoints);
            _elementEffectsBarController.UpdateEffects(Wizard.ElementEffect);
        }
    }
}
