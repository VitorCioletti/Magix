namespace Magix.Domain.Interface.Board
{
    using System.Collections.Generic;
    using NatureElements;
    using Result;

    public interface ITile
    {
        IPosition Position { get; }

        List<INatureElement> NatureElements { get; }

        List<IMixResult> Mix(INatureElement natureElement, List<ITile> alreadyMixedTiles = null);

        bool HasElement<T>() where T : INatureElement;

        bool CanReactOnSelf(INatureElement natureElement);

        void SetAdjacent(List<ITile> adjacent);
    }
}
