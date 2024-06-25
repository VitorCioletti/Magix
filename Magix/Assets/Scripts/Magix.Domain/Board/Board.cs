namespace Magix.Domain.Board
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Element;
    using Interface;
    using Interface.Board;
    using Interface.Board.Result;
    using Interface.Element;
    using Interface.Element.Result;
    using Result;

    public class Board : IBoard
    {
        public ITile[,] Tiles => _grid.Tiles;

        public List<IPlayer> Players { get; private set; }

        public IPlayer CurrentPlayer { get; private set; }

        private readonly Grid _grid;

        private readonly Queue<IPlayer> _orderToPlay;

        public Board(List<IPlayer> players)
        {
            Players = players;
            _grid = new Grid();
            _orderToPlay = new Queue<IPlayer>(players);

            _setNextPlayerToPlay();
        }

        public bool CanAttack(IWizard wizard)
        {
            IList<ITile> previewArea = GetPreviewArea(wizard, WizardActionType.Attack);

            bool canAttack = false;

            foreach (ITile tile in previewArea)
            {
                foreach (IPlayer player in Players)
                {
                    foreach (IWizard playerWizard in player.Wizards)
                    {
                        if (playerWizard.Position.Equals(tile.Position))
                            canAttack = true;
                    }
                }
            }

            return canAttack;
        }

        public ICastResult CastElement(IWizard wizard, IElement element, IList<ITile> tiles)
        {
            _verifyWizardBelongsCurrentPlayer(wizard);

            var allResults = new List<IMixResult>();

            foreach (ITile tile in tiles)
            {
                List<IMixResult> mixResults = tile.Mix(element);

                allResults.AddRange(mixResults);
            }

            wizard.RemoveRemainingActions(tiles.Count);

            _tryChangeCurrentPlayer();

            var castResult = new CastResult(allResults);

            IPlayer winner = _tryGetWinner();
            castResult.SetWinner(winner);

            return castResult;
        }

        public IAttackResult Attack(IWizard wizard, IPosition position)
        {
            IEnumerable<IWizard> allWizards = Players.SelectMany(p => p.Wizards);

            IWizard wizardInPosition = allWizards.FirstOrDefault(w => w.Position.Equals(position));

            var attackResult = new AttackResult(
                false,
                null,
                null,
                IAttackResult.NoWizardInPosition);

            if (wizardInPosition is not null)
            {
                IEffectResult effectResult = wizardInPosition.TakeDamage(IWizard.AttackDamage);

                attackResult = new AttackResult(true, effectResult, wizardInPosition);
            }

            IPlayer winner = _tryGetWinner();
            attackResult.SetWinner(winner);

            return attackResult;
        }

        // TODO: Pass positions instead of tiles.
        public IMovementResult Move(IWizard wizard, IList<ITile> tiles)
        {
            _verifyWizardBelongsCurrentPlayer(wizard);

            var steps = new List<IStepResult>();

            var movementResult = new MovementResult(steps, true, string.Empty);

            foreach (ITile tile in tiles)
            {
                if (tile.Position.Equals(wizard.Position))
                {
                    movementResult = new MovementResult(
                        new List<IStepResult>(),
                        false,
                        IMovementResult.CantGoThroughBlockingElement);
                }

                foreach (IElement natureElement in tile.Element)
                {
                    if (natureElement.Blocking)
                    {
                        movementResult = new MovementResult(
                            new List<IStepResult>(),
                            false,
                            IMovementResult.CantGoThroughBlockingElement);
                    }

                    IEffectResult effectResult = natureElement.ApplyElementEffect(wizard);

                    var step = new StepResult(tile, effectResult);

                    steps.Add(step);
                }
            }

            wizard.RemoveRemainingActions(tiles.Count);

            wizard.Position = tiles.Last().Position;

            _tryChangeCurrentPlayer();

            IPlayer winner = _tryGetWinner();
            movementResult.SetWinner(winner);

            return movementResult;
        }

        public List<IElement> GetElementToCast()
        {
            return new List<IElement> {new Fire(), new Water(), new Wind(),};
        }

        // TODO: Pass position instead of tile.
        public bool HasWizard(ITile tile)
        {
            IWizard wizard = GetWizard(tile);

            return wizard != null;
        }

        public IWizard GetWizard(ITile tile)
        {
            IWizard wizardInTile = null;

            foreach (IPlayer player in Players)
            {
                foreach (IWizard wizard in player.Wizards)
                {
                    if (wizard.Position.Equals(tile.Position))
                        wizardInTile = wizard;
                }
            }

            return wizardInTile;
        }

        public bool BelongsToCurrentPlayer(IWizard wizard)
        {
            List<IWizard> wizards = CurrentPlayer.Wizards;

            return wizards.Contains(wizard);
        }

        public IList<ITile> GetPreviewArea(IWizard wizard, WizardActionType actionType)
        {
            ITile tile = Tiles[wizard.Position.X, wizard.Position.Y];

            int distance = wizard.GetDistance(actionType);

            return _grid.GetPreviewArea(tile, distance);
        }

        public IList<ITile> GetPreviewPathTo(IWizard wizard, ITile objectiveTile)
        {
            ITile tile = Tiles[wizard.Position.X, wizard.Position.Y];

            return _grid.GetPreviewPathTo(tile, wizard.RemainingActions, objectiveTile);
        }

        private void _verifyWizardBelongsCurrentPlayer(IWizard wizard)
        {
            if (!BelongsToCurrentPlayer(wizard))
                throw new InvalidOperationException("Wizard does not belong to current player.");
        }

        private IPlayer _tryGetWinner()
        {
            IPlayer player1 = Players[0];
            IPlayer player2 = Players[1];

            List<IWizard> wizardsPlayer1 = player1.Wizards;
            List<IWizard> wizardsPlayer2 = player2.Wizards;

            IPlayer winner = null;

            bool player1Lost = wizardsPlayer1.All(w => w.IsDead);
            bool player2Lost = wizardsPlayer2.All(w => w.IsDead);

            if (player1Lost)
            {
                winner = player2;
            }
            else if (player2Lost)
            {
                winner = player1;
            }

            return winner;
        }

        private void _tryChangeCurrentPlayer()
        {
            if (!CurrentPlayer.HasRemainingActions)
                _setNextPlayerToPlay();
        }

        private void _setNextPlayerToPlay()
        {
            IPlayer nextPlayer = _orderToPlay.Dequeue();
            _orderToPlay.Enqueue(nextPlayer);

            CurrentPlayer = nextPlayer;

            foreach (IWizard wizard in CurrentPlayer.Wizards)
                wizard.ResetRemainingActions();
        }
    }
}
