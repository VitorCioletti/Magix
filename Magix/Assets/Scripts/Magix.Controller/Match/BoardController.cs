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

        private List<WizardController> _wizards;

        private StateMachineManager _stateMachine;

        private IMatchService _matchService;

        public void Move(IWizard wizard, List<ITile> tiles)
        {
            WizardController wizardController = _wizards.First(w => w.Wizard == wizard);

            foreach (ITile tile in tiles)
            {
                TileController tileController = _gridController.Tiles[tile.Position.X, tile.Position.Y];

                wizardController.Move(tileController);
            }
        }

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
            _wizards = new List<WizardController>();

            _gridController.Init(
                _matchService.Board.Tiles,
                _onMouseEntered,
                _onMouseExited,
                _onTileClicked);

            foreach (IPlayer player in _matchService.Board.Players)
                _createWizards(player.Wizards);

            _initializeStateMachine();

            _fixPosition();
        }

        private void _initializeStateMachine()
        {
            _stateMachine = new StateMachineManager(this, _matchService);

            _stateMachine.Push(new IdleState(_gridController));
        }

        private void _createWizards(List<IWizard> wizardsPositions)
        {
            foreach (IWizard wizard in wizardsPositions)
            {
                IPosition position = wizard.Position;

                TileController tileToSpawnWizard = _gridController.Tiles[position.X, position.Y];
                WizardController wizardController = Instantiate(_wizardPrefab, transform);

                wizardController.Init(wizard, tileToSpawnWizard);

                _wizards.Add(wizardController);
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
