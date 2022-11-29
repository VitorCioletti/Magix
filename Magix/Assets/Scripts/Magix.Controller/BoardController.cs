namespace Magix.Controller
{
    using System.Collections.Generic;
    using StateMachine;
    using StateMachine.States;
    using TMPro;
    using UnityEngine;

    public class BoardController : MonoBehaviour
    {
        [field: SerializeField]
        private GridController _gridController { get; set; } = default;

        [field: SerializeField]
        private WizardController _wizardPrefab { get; set; } = default;

        [field: SerializeField]
        private TextMeshProUGUI _currentStateText { get; set; } = default;

        private StateMachineManager _stateMachine;

        private void Update()
        {
            _currentStateText.text = _stateMachine.GetCurrentState().GetType().Name;
        }

        private void Start()
        {
            _init();
        }

        private void _init()
        {
            _gridController.Init(_onMouseEntered, _onMouseExited, _onTileClicked);

            // TODO: Get positions from domain.
            var wizardsPositions = new List<Vector2> {new(0, 7), new(2, 9),};
            _createWizards(wizardsPositions);

            _initializeStateMachine();
        }

        private void _initializeStateMachine()
        {
            _stateMachine = new StateMachineManager();

            _stateMachine.Push(new IdleState());
        }

        private void _createWizards(List<Vector2> wizardsPositions)
        {
            foreach (Vector2 wizardsPosition in wizardsPositions)
            {
                TileController tileToSpawnWizard =
                    _gridController.Tiles[(int)wizardsPosition.x, (int)wizardsPosition.y];

                WizardController wizardController = Instantiate(_wizardPrefab, transform);

                var wizardOffset = new Vector3(0, 0.30f, 0);
                Vector3 wizardPosition = tileToSpawnWizard.transform.position + wizardOffset;

                wizardController.transform.position = wizardPosition;

                // TODO: Get highest sorting order from grid
                wizardController.SpriteRenderer.sortingOrder = 20;
            }
        }

        private void _onTileClicked(TileController tileController)
        {
            _stateMachine.GetCurrentState().OnClickTile(tileController);
        }

        private void _onMouseEntered(TileController tileController)
        {
            _stateMachine.GetCurrentState().OnMouseEntered(tileController);
        }

        private void _onMouseExited(TileController tileController)
        {
            _stateMachine.GetCurrentState().OnMouseExited(tileController);
        }

    }
}
