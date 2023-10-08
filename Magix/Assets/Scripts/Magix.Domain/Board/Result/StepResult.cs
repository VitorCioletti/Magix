namespace Magix.Domain.Board.Result
{
    using Interface.Board;
    using Interface.Board.Result;
    using Interface.NatureElements.Result;

    public class StepResult : IStepResult
    {
        public ITile Tile { get; private set; }

        public IEffectResult Effect { get; private set; }

        public StepResult(ITile tile, IEffectResult effect)
        {
            Effect = effect;
            Tile = tile;
        }
    }
}
