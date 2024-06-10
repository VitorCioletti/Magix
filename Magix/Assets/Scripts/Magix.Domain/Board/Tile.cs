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

        public List<INatureElement> NatureElements { get; private set; }

        private List<ITile> _adjacentTiles;

        public Tile(IPosition position)
        {
            NatureElements = new List<INatureElement> {new Natural()};
            Position = position;

            _adjacentTiles = new List<ITile>();
        }

        public bool HasElement<T>() where T : INatureElement
        {
            foreach (INatureElement natureElement in NatureElements)
            {
                if (natureElement is T)
                    return true;
            }

            return false;
        }

        public bool CanReactOnSelf(INatureElement natureElement)
        {
            return NatureElements.Any(a => a.CanReact(natureElement));
        }

        public void SetAdjacent(List<ITile> adjacent)
        {
            _adjacentTiles = adjacent;
        }

        public List<IMixResult> Mix(INatureElement natureElement, List<ITile> alreadyMixedTiles = null)
        {
            alreadyMixedTiles ??= new List<ITile>();

            if (alreadyMixedTiles.Contains(this))
                return null;

            alreadyMixedTiles.Add(this);

            List<IMixResult> mixResults = _mixOnSelf(natureElement);

            if (mixResults.Exists(m => m.Success) && !natureElement.CanStack)
                NatureElements.Clear();

            foreach (IMixResult mixResult in mixResults)
            {
                if (mixResult.Success)
                {
                    NatureElements.Add(mixResult.NewElement);
                }
            }

            if (natureElement.CanSpread)
            {
                List<IMixResult> adjacentAffectedTiles = _tryMixOnAdjacent(natureElement, alreadyMixedTiles);

                mixResults.AddRange(adjacentAffectedTiles);
            }

            return mixResults;
        }

        private List<IMixResult> _mixOnSelf(INatureElement elementToMix)
        {
            var mixResults = new List<IMixResult>();

            foreach (INatureElement elementOnTile in NatureElements)
            {
                INatureElement mixedElement = elementOnTile.GetMixedElement(elementToMix);

                IMixResult result;

                if (mixedElement is not null)
                {
                    result = new MixResult(
                        this,
                        elementToMix,
                        elementOnTile,
                        mixedElement);
                }
                else
                {
                    result = new MixResult(
                        this,
                        elementToMix,
                        elementOnTile,
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
            List<ITile> alreadyMixedTiles)
        {
            var allResults = new List<IMixResult>();

            foreach (ITile tile in _adjacentTiles)
            {
                if (alreadyMixedTiles.Contains(tile))
                    continue;

                List<IMixResult> mixResults = tile.Mix(natureElement, alreadyMixedTiles);

                if (mixResults != null)
                    allResults.AddRange(mixResults);

                alreadyMixedTiles.Add(tile);
            }

            return allResults;
        }
    }
}
