namespace Magix.Controller.Match.Board
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DependencyInjection;
    using Domain;
    using Domain.Interface;
    using Domain.Interface.Board;
    using Domain.Interface.Board.Result;
    using Domain.Interface.Element;
    using Element;
    using Service.Interface;
    using StateMachine;
    using StateMachine.States;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using Wizard;

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
        private ElementMenuBarController _elementMenuBarController { get; set; }

        [field: SerializeField]
        private Button _cancelButton { get; set; }

        [field: SerializeField]
        private Button _executeButton { get; set; }

        private List<WizardController> _wizards;

        private StateMachineManager _stateMachine;

        private IMatchService _matchService;

        public async Task<IMovementResult> MoveAsync(IWizard wizard, IList<ITile> tiles)
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

            return movementResult;
        }

        public async Task<List<IAttackResult>> AttackAsync(IWizard wizard, IList<ITile> tiles)
        {
            var results = new List<IAttackResult>();

            foreach (ITile tile in tiles)
            {
                IAttackResult attackResult = _matchService.Board.Attack(wizard, tile.Position);

                if (attackResult.Success)
                {
                    WizardController attacker = _getWizardController(wizard);
                    WizardController attacked = _getWizardController(attackResult.Target);

                    Task attackTask = attacker.AttackAsync(tile);
                    Task receivingAttackTask = attacked.TakeDamageAsync(attackResult.EffectResult.DamageTaken);

                    results.Add(attackResult);

                    await Task.WhenAll(attackTask, receivingAttackTask);
                }
                else
                    throw new InvalidOperationException($"\"{attackResult.ErrorId}\".");
            }

            return results;
        }

        public async Task<ICastResult> CastElementAsync(IWizard wizard, IElement element, IList<ITile> tiles)
        {
            ICastResult castResult = _matchService.Board.CastElement(wizard, element, tiles);

            if (castResult.Success)
            {
                WizardController wizardController = _getWizardController(wizard);

                await wizardController.CastAsync();

                foreach (IMixResult mixResult in castResult.ResultedMixes)
                {
                    ITile affectedTile = mixResult.AffectedTile;

                    TileController tileController = _getTileController(affectedTile);
                    tileController.Tile = affectedTile;

                    tileController.UpdateElements();
                }

            }
            else
                throw new InvalidOperationException($"\"{castResult.ErrorId}\".");

            return castResult;
        }

        public void EnableCancelButton(bool enable)
        {
            _cancelButton.gameObject.SetActive(enable);
        }

        public void EnableActionSelectionButtons(IWizard wizard, bool enable)
        {
            EnableActionSelectionButtons(enable);

            bool canAttack = _matchService.Board.CanAttack(wizard);

            _wizardActionsMenuBarMenuBar.EnableAttackButton(canAttack);
        }

        public void ShowEndGame(IPlayer winner)
        {
            throw new NotImplementedException();
        }

        public void Restart()
        {
            throw new NotImplementedException();
        }

        public void EnableActionSelectionButtons(bool enable)
        {
            _wizardActionsMenuBarMenuBar.gameObject.SetActive(enable);
        }

        public void EnableElementMenuBar(bool enable)
        {
            _elementMenuBarController.gameObject.SetActive(enable);
        }

        public void EnableExecuteButton(bool enable)
        {
            _executeButton.gameObject.SetActive(enable);
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
                _createWizards(player);

            _initializeStateMachine();

            List<IElement> natureElementsToCast = _matchService.Board.GetElementToCast();

            _wizardActionsMenuBarMenuBar.Initialize(
                _onClickMoveAction,
                _onClickAttackAction,
                _onClickApplyElementAction);

            _elementMenuBarController.Initialize(natureElementsToCast, _onClickElementButton);

            EnableActionSelectionButtons(false);
            EnableElementMenuBar(false);
            EnableExecuteButton(false);

            _fixPosition();

            _cancelButton.onClick.AddListener(_onClickCancel);
            _executeButton.onClick.AddListener(_onClickExecute);
        }

        private void _initializeStateMachine()
        {
            _stateMachine = new StateMachineManager(this, _matchService, _onChangeState);

            _stateMachine.Push(new IdleState());
        }

        private void _createWizards(IPlayer player)
        {
            foreach (IWizard wizard in player.Wizards)
            {
                IPosition position = wizard.Position;

                TileController tileToSpawnWizard = GridController.Tiles[position.X, position.Y];
                WizardController wizardController = Instantiate(_wizardPrefab, transform);

                wizardController.Initialize(wizard, tileToSpawnWizard, player.Number);

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

        private void _onChangeState(BaseState currentState)
        {
            string stateName = currentState?.GetType().Name;

            Debug.Log($"Changed state to \"{stateName}\".");

            _currentStateText.text = stateName;
        }

        private void _onClickCancel()
        {
            _stateMachine.GetCurrentState().OnClickCancel();
        }

        private void _onClickExecute()
        {
            _stateMachine.GetCurrentState().OnClickExecute();
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
            _stateMachine.GetCurrentState().OnClickWizardAction(WizardActionType.Move);
        }

        private void _onClickAttackAction()
        {
            _stateMachine.GetCurrentState().OnClickWizardAction(WizardActionType.Attack);
        }

        private void _onClickApplyElementAction()
        {
            _stateMachine.GetCurrentState().OnClickWizardAction(WizardActionType.CastElement);
        }

        private void _onClickElementButton(ElementButtonController elementButtonController)
        {
            _stateMachine.GetCurrentState().OnClickElement(elementButtonController);
        }
    }
}
