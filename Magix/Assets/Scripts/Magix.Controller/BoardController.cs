namespace Magix.Controller
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class BoardController : MonoBehaviour
    {
        [field: SerializeField]
        private GridController _gridController { get; set; } = default;

        [field: SerializeField]
        private WizardController _wizardPrefab { get; set; } = default;

        private void Start()
        {
            _gridController.Init();

            var wizardsPositions = new List<Tuple<int, int>>
            {
                new (0, 7),
                new (2, 9),
            };

            foreach (Tuple<int, int> wizardsPosition in wizardsPositions)
            {
                TileController tileToSpawnWizard = _gridController.Tiles[wizardsPosition.Item1, wizardsPosition.Item2];

                WizardController wizardController = Instantiate(_wizardPrefab, transform);

                var wizardOffset = new Vector3(0, 0.30f, 0);
                Vector3 wizardPosition = tileToSpawnWizard.transform.position + wizardOffset;

                wizardController.transform.position = wizardPosition;

                wizardController.SpriteRenderer.sortingOrder = 10;
            }
        }
    }
}
