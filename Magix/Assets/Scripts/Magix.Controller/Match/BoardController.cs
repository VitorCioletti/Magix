namespace Magix.Controller.Match
{
    using System.Collections.Generic;
    using System.Linq;
    using DependencyInjection;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Service.Interface;
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

        private IMatchService _matchService;

        private void Update()
        {
            // TODO: Remove from update and add it to the state machine.
            BaseState currentState = _stateMachine.GetCurrentState();

            _currentStateText.text = currentState?.GetType().Name;
        }

        private void Start()
        {
            _init();
        }

        private void _init()
        {
            _matchService = Resolver.GetService<IMatchService>();

            _gridController.Init(
                _matchService.Board.Tiles,
                _onMouseEntered,
                _onMouseExited,
                _onTileClicked);

            foreach (Dictionary<IWizard, IPosition> wizardsPositions in _matchService.Board.WizardsPositions.Values)
            {
                IEnumerable<Vector2> positions = wizardsPositions.Values.Select(p => new Vector2(p.X, p.Y));

                _createWizards(positions);
            }

            _initializeStateMachine();

            _fixPosition();
        }

        private void _initializeStateMachine()
        {
            _stateMachine = new StateMachineManager(_matchService);

            _stateMachine.Push(new IdleState(_gridController));
        }

        private void _createWizards(IEnumerable<Vector2> wizardsPositions)
        {
            foreach (Vector2 wizardsPosition in wizardsPositions)
            {
                TileController tileToSpawnWizard =
                    _gridController.Tiles[(int)wizardsPosition.x, (int)wizardsPosition.y];

                WizardController wizardController = Instantiate(_wizardPrefab, transform);

                var wizardOffset = new Vector3(0, 0.30f, 0);
                Vector3 wizardPosition = tileToSpawnWizard.transform.position + wizardOffset;

                wizardController.transform.position = wizardPosition;
            }
        }

        private void _fixPosition()
        {
            TileController[,] tiles = _gridController.Tiles;

            int lastTileIndex = tiles.GetUpperBound(0);

            TileController firstTile = tiles[0, 0];
            TileController lastTile = tiles[lastTileIndex, lastTileIndex];

            float firstTileXPosition = firstTile.transform.position.x;
            float lastTileTileXPosition = lastTile.transform.position.x;

            float newX = (firstTileXPosition - lastTileTileXPosition) / 2;

            gameObject.transform.position = new Vector3(newX, 0, 0);
        }

        private void _onTileClicked(TileController tileController)
        {
            _stateMachine.GetCurrentState().OnClickTile(tileController);
        }

        private void _onMouseEntered(TileController tileController)
        {
            _stateMachine.GetCurrentState().OnEnterMouse(tileController);
        }

        private void _onMouseExited(TileController tileController)
        {
            _stateMachine.GetCurrentState().OnExitMouse(tileController);
        }
    }
}
