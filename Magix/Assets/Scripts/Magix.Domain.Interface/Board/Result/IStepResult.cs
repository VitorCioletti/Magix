namespace Magix.Domain.Interface.Board.Result
{
    using NatureElements.Result;

    public interface IStepResult
    {
        public ITile Tile { get; }

        public IEffectResult Effect { get; }
    }
}
