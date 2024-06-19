namespace Magix.Domain.Interface.Board.Result
{
    using Element.Result;

    public interface IStepResult
    {
        public ITile Tile { get; }

        public IEffectResult Effect { get; }
    }
}
