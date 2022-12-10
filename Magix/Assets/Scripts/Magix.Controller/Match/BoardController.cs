namespace Magix.Controller.Match
{
    using System.Collections.Generic;
    using System.Linq;
    using DependencyInjection;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Domain.Interface.NatureElements;
    using Service.Interface;
    using StateMachine;
    using StateMachine.States;
    using TMPro;
    using UnityEngine;

    public class BoardController : MonoBehaviour
    {
        [field: SerializeField]
        public GridController GridController { get; set; }

        [field: SerializeField]
        private WizardController _wizardPrefab { get; set; }

        [field: SerializeField]
        private TextMeshProUGUI _currentStateText { get; set; }

        [field: SerializeField]
        private WizardActionsMenuBarController _wizardActionsMenuBarMenuBar { get; set; }

        private List<WizardController> _wizards;

        private StateMachineManager _stateMachine;

        private IMatchService _matchService;

        public void Move(IWizard wizard, List<ITile> tiles)
        {
            WizardController wizardController = _wizards.First(w => w.Wizard == wizard);

            foreach (ITile tile in tiles)
            {
                TileController tileController = _getTileController(tile);

                wizardController.Move(tileController);
            }
        }

        public void ApplyNatureElement(IWizard _, INatureElement natureElement, List<ITile> tiles)
        {
            foreach (ITile tile in tiles)
            {
                TileController tileController = _getTileController(tile);

                tileController.ApplyNatureElement(natureElement);
            }
        }

        public void EnableActionSelectionButtons(bool enable)
        {
            _wizardActionsMenuBarMenuBar.gameObject.SetActive(enable);
        }

        private void Update()
        {
            // TODO: Remove from update and add it to the state machine.
            BaseState currentState = _stateMachine.GetCurrentState();

            _currentStateText.text = currentState?.GetType().Name;
        }

        private void Start()
        {
            _initialize();
        }

        private TileController _getTileController(ITile tile)
        {
            return GridController.Tiles[tile.Position.X, tile.Position.Y];
        }

        private void _initialize()
        {
            _matchService = Resolver.GetService<IMatchService>();
            _wizards = new List<WizardController>();

            GridController.Init(
                _matchService.Board.Tiles,
                _onMouseEntered,
                _onMouseExited,
                _onTileClicked);

            foreach (IPlayer player in _matchService.Board.Players)
                _createWizards(player.Wizards);

            _initializeStateMachine();
            _wizardActionsMenuBarMenuBar.Initialize(_onClickMoveAction, _onClickApplyNatureElementAction);

            EnableActionSelectionButtons(false);

            _fixPosition();
        }

        private void _initializeStateMachine()
        {
            _stateMachine = new StateMachineManager(this, _matchService);

            _stateMachine.Push(new IdleState());
        }

        private void _createWizards(List<IWizard> wizards)
        {
            var color = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
            );

            foreach (IWizard wizard in wizards)
            {
                IPosition position = wizard.Position;

                TileController tileToSpawnWizard = GridController.Tiles[position.X, position.Y];
                WizardController wizardController = Instantiate(_wizardPrefab, transform);

                wizardController.Initialize(wizard, tileToSpawnWizard, color);

                _wizards.Add(wizardController);
            }
        }

        private void _fixPosition()
        {
            TileController[,] tiles = GridController.Tiles;

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

        private void _onClickMoveAction()
        {
            _stateMachine.GetCurrentState().OnClickActionMove();
        }

        private void _onClickApplyNatureElementAction()
        {
            _stateMachine.GetCurrentState().OnClickActionApplyNatureElement();
        }
    }
}
