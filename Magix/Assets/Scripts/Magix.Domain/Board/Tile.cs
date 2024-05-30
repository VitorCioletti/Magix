namespace Magix.Domain.Board
{
    using System.Collections.Generic;
    using System.Linq;
    using Interface.Board;
    using Interface.Board.Result;
    using Interface.NatureElements;
    using NatureElements;
    using Result;

    public class Tile : ITile
    {
        public IPosition Position { get; private set; }

        // TODO: Add properties to hold element on ground and air.

        public List<INatureElement> Elements { get; private set; }

        private List<ITile> _adjacentTiles;

        public Tile(IPosition position)
        {
            Elements = new List<INatureElement> {new Natural()};
            Position = position;

            _adjacentTiles = new List<ITile>();
        }

        public bool HasElement<T>() where T : INatureElement
        {
            foreach (INatureElement natureElement in Elements)
            {
                if (natureElement is T)
                    return true;
            }

            return false;
        }

        public bool CanReactOnSelf(INatureElement natureElement)
        {
            return Elements.Any(a => a.CanReact(natureElement));
        }

        public void SetAdjacent(List<ITile> adjacent)
        {
            _adjacentTiles = adjacent;
        }

        public List<IMixResult> Mix(
            INatureElement natureElement,
            List<ITile> alreadyMixedTiles = null,
            int depth = int.MaxValue)
        {
            if (depth == 0)
                return null;

            depth--;

            alreadyMixedTiles ??= new List<ITile>();

            if (alreadyMixedTiles.Contains(this))
                return null;

            alreadyMixedTiles.Add(this);

            List<IMixResult> mixResults = _mixOnSelf(natureElement);

            if (mixResults.Exists(m => m.Success))
            {
                Elements.Clear();
            }

            foreach (IMixResult mixResult in mixResults)
            {
                if (mixResult.Success)
                {
                    Elements.Add(mixResult.NewElement);
                }
            }

            if (natureElement.CanSpread)
            {
                List<IMixResult> adjacentAffectedTiles = _tryMixOnAdjacent(natureElement, alreadyMixedTiles, depth);

                mixResults.AddRange(adjacentAffectedTiles);
            }

            return mixResults;
        }

        private List<IMixResult> _mixOnSelf(INatureElement elementToMix)
        {
            var mixResults = new List<IMixResult>();

            foreach (INatureElement element in Elements)
            {
                INatureElement mixedElement = element.GetMixedElement(elementToMix);

                IMixResult result;

                if (mixedElement is not null)
                {
                    result = new MixResult(
                        this,
                        elementToMix,
                        element,
                        mixedElement);
                }
                else
                {
                    result = new MixResult(
                        this,
                        elementToMix,
                        element,
                        null,
                        false,
                        "cant-mix");
                }

                mixResults.Add(result);
            }

            return mixResults;
        }

        private List<IMixResult> _tryMixOnAdjacent(
            INatureElement natureElement,
            List<ITile> alreadyMixedTiles,
            int depth)
        {
            var allResults = new List<IMixResult>();

            foreach (ITile tile in _adjacentTiles)
            {
                if (alreadyMixedTiles.Contains(tile))
                    continue;

                if (!tile.CanReactOnSelf(natureElement))
                    continue;

                alreadyMixedTiles.Add(tile);

                List<IMixResult> mixResults = tile.Mix(natureElement, alreadyMixedTiles, depth);

                allResults.AddRange(mixResults);
            }

            return allResults;
        }
    }
}
