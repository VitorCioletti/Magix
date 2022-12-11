namespace Magix.Controller.Match.Board
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DependencyInjection;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Domain.Interface.Board.Result;
    using Domain.Interface.NatureElements;
    using NatureElements;
    using Service.Interface;
    using StateMachine;
    using StateMachine.States;
    using TMPro;
    using UnityEngine;
    using Wizard;
    using Random = UnityEngine.Random;

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

        [field: SerializeField]
        private NatureElementsMenuBarController _natureElementsMenuBarController { get; set; }

        [field: SerializeField]
        private CastNatureElementButtonController _castNatureElementButtonController { get; set; }

        private List<WizardController> _wizards;

        private StateMachineManager _stateMachine;

        private IMatchService _matchService;

        public async Task MoveAsync(IWizard wizard, List<ITile> tiles)
        {
            IMovementResult movementResult = _matchService.Board.Move(wizard, tiles);

            if (movementResult.Success)
            {
                WizardController wizardController = _wizards.First(w => w.Wizard == wizard);

                List<TileController> tilesController = tiles.Select(_getTileController).ToList();

                await wizardController.MoveAsync(tilesController);
            }
            else
                throw new InvalidOperationException($"\"{movementResult.ErrorId}\".");
        }

        public async Task CastNatureElementAsync(IWizard wizard, INatureElement natureElement, List<ITile> tiles)
        {
            IApplyNatureElementResult applyNatureElementResult =
                _matchService.Board.ApplyNatureElement(wizard, natureElement, tiles);

            if (applyNatureElementResult.Success)
            {
                WizardController wizardController = _getWizardController(wizard);

                await wizardController.CastAsync();

                foreach (ITile tile in applyNatureElementResult.Tiles)
                {
                    TileController tileController = _getTileController(tile);
                    tileController.Tile = tile;

                    tileController.UpdateNatureElement();
                }
            }
            else
                throw new InvalidOperationException($"\"{applyNatureElementResult.ErrorId}\".");
        }

        public void EnableActionSelectionButtons(bool enable)
        {
            _wizardActionsMenuBarMenuBar.gameObject.SetActive(enable);
        }

        public void EnableCastNatureElementButton(bool enable)
        {
            _castNatureElementButtonController.gameObject.SetActive(enable);
        }

        public void EnableNatureElementsMenuBar(bool enable)
        {
            _natureElementsMenuBarController.gameObject.SetActive(enable);
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

        private WizardController _getWizardController(IWizard wizard)
        {
            return _wizards.FirstOrDefault(w => w.Wizard == wizard);
        }

        private void _initialize()
        {
            _matchService = Resolver.GetService<IMatchService>();
            _wizards = new List<WizardController>();

            GridController.Initialize(
                _matchService.Board.Tiles,
                _onMouseEntered,
                _onMouseExited,
                _onTileClicked);

            foreach (IPlayer player in _matchService.Board.Players)
                _createWizards(player.Wizards);

            _initializeStateMachine();

            List<INatureElement> natureElementsToCast = _matchService.Board.GetNatureElementsToCast();

            _wizardActionsMenuBarMenuBar.Initialize(_onClickMoveAction, _onClickApplyNatureElementAction);
            _castNatureElementButtonController.Initialize(_onClickCastNatureElement);
            _natureElementsMenuBarController.Initialize(natureElementsToCast, _onClickNatureElementButton);

            EnableActionSelectionButtons(false);
            EnableCastNatureElementButton(false);
            EnableNatureElementsMenuBar(false);

            _fixPosition();
        }

        private void _initializeStateMachine()
        {
            _stateMachine = new StateMachineManager(this, _matchService);

            _stateMachine.Push(new IdleState());
        }

        private void _createWizards(List<IWizard> wizards)
        {
            foreach (IWizard wizard in wizards)
            {
                IPosition position = wizard.Position;

                TileController tileToSpawnWizard = GridController.Tiles[position.X, position.Y];
                WizardController wizardController = Instantiate(_wizardPrefab, transform);

                wizardController.Initialize(wizard, tileToSpawnWizard);

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

        private void _onClickCastNatureElement()
        {
            _stateMachine.GetCurrentState().OnClickCastNatureElement();
        }

        private void _onClickNatureElementButton(NatureElementButtonController natureElementButtonController)
        {
            _stateMachine.GetCurrentState().OnClickNatureElementButton(natureElementButtonController);
        }
    }
}
