namespace Magix.Domain.Interface.Board
{
    using System.Collections.Generic;
    using NatureElements;
    using Result;

    public interface ITile
    {
        IPosition Position { get; }

        List<INatureElement> Elements { get; }

        List<IMixResult> Mix(
            INatureElement natureElement,
            List<ITile> alreadyMixedTiles = null,
            int depth = int.MaxValue);

        bool HasElement<T>() where T : INatureElement;

        bool CanReactOnSelf(INatureElement natureElement);

        void SetAdjacents(List<ITile> adjacents);
    }
}
