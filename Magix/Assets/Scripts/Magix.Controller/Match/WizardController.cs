namespace Magix.Controller.Match
{
    using Domain.Interface;
    using UnityEngine;

    public class WizardController : MonoBehaviour
    {
        [field: SerializeField]
        public SpriteRenderer SpriteRenderer { get; private set; } = default;

        public IWizard Wizard { get; private set; }

        private Vector3 _offsetPosition;

        public void Initialize(IWizard wizard, TileController tileToSpawnWizard)
        {
            _offsetPosition = new Vector3(0, 0.30f, 0);

            Wizard = wizard;

            Move(tileToSpawnWizard);
        }

        public void Move(TileController tileController)
        {
            Vector3 wizardPosition = tileController.transform.position + _offsetPosition;

            transform.position = wizardPosition;
        }
    }
}
