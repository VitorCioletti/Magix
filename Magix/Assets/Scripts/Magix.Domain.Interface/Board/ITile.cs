namespace Magix.Domain.Interface.Board
{
    using System.Collections.Generic;
    using Element;
    using Result;

    public interface ITile
    {
        IPosition Position { get; }

        List<IElement> Element { get; }

        List<IMixResult> Mix(IElement element, List<ITile> alreadyMixedTiles = null);

        bool HasElement<T>() where T : IElement;

        bool CanReactOnSelf(IElement element);

        void SetAdjacent(List<ITile> adjacent);
    }
}
