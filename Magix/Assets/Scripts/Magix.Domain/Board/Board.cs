namespace Magix.Domain.Board
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interface;
    using Interface.Board;
    using Interface.Board.Result;
    using Interface.NatureElements;
    using Interface.NatureElements.Result;
    using NatureElements;
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

        // TODO: Pass positions instead of tiles.
        public ICastResult CastNatureElement(IWizard wizard, INatureElement natureElement, IList<ITile> tiles)
        {
            _verifyWizardBelongsCurrentPlayer(wizard);

            var allResults = new List<IMixResult>();

            foreach (ITile tile in tiles)
            {
                List<IMixResult> mixResults = tile.Mix(natureElement);

                allResults.AddRange(mixResults);
            }

            wizard.RemoveRemainingActions(tiles.Count);

            _tryChangeCurrentPlayer();

            return new CastResult(allResults);
        }

        public IAttackResult Attack(IWizard wizard, IPosition position)
        {
            IEnumerable<IWizard> allWizards = Players.SelectMany(p => p.Wizards);

            IWizard wizardInPosition = allWizards.FirstOrDefault(w => w.Position == position);

            var attackResult = new AttackResult(false, null, IAttackResult.NoWizardInPosition);

            if (wizardInPosition is not null)
            {
                IEffectResult effectResult = wizardInPosition.TakeDamage(IWizard.AttackDamage);

                attackResult = new AttackResult(true, effectResult);
            }

            return attackResult;
        }

        // TODO: Pass positions instead of tiles.
        public IMovementResult Move(IWizard wizard, IList<ITile> tiles)
        {
            _verifyWizardBelongsCurrentPlayer(wizard);

            var steps = new List<IStepResult>();

            foreach (ITile tile in tiles)
            {
                foreach (INatureElement natureElement in tile.NatureElements)
                {
                    if (natureElement.Blocking)
                    {
                        return new MovementResult(
                            new List<IStepResult>(),
                            false,
                            MovementResult.CantGoThroughBlockingElement);
                    }

                    IEffectResult effectResult = natureElement.ApplyElementEffect(wizard);

                    var step = new StepResult(tile, effectResult);

                    steps.Add(step);
                }
            }

            wizard.RemoveRemainingActions(tiles.Count);

            wizard.Position = tiles.Last().Position;

            _tryChangeCurrentPlayer();

            return new MovementResult(steps, true, string.Empty);
        }

        public List<INatureElement> GetNatureElementsToCast()
        {
            return new List<INatureElement> {new Fire(), new Water(), new Wind(),};
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

        public IList<ITile> GetPreviewArea(IWizard wizard)
        {
            ITile tile = Tiles[wizard.Position.X, wizard.Position.Y];

            return _grid.GetPreviewArea(tile, wizard.RemainingActions);
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
