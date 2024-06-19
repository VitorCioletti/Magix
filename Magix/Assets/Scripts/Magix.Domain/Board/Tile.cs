namespace Magix.Domain.Board
{
    using System.Collections.Generic;
    using System.Linq;
    using Interface.Board;
    using Interface.Board.Result;
    using Interface.Element;
    using Element;
    using Result;

    public class Tile : ITile
    {
        public IPosition Position { get; private set; }

        // TODO: Add properties to hold element on ground and air.

        public List<IElement> Element { get; private set; }

        private List<ITile> _adjacentTiles;

        public Tile(IPosition position)
        {
            Element = new List<IElement> {new Natural()};
            Position = position;

            _adjacentTiles = new List<ITile>();
        }

        public bool HasElement<T>() where T : IElement
        {
            foreach (IElement natureElement in Element)
            {
                if (natureElement is T)
                    return true;
            }

            return false;
        }

        public bool CanReactOnSelf(IElement element)
        {
            return Element.Any(a => a.CanReact(element));
        }

        public void SetAdjacent(List<ITile> adjacent)
        {
            _adjacentTiles = adjacent;
        }

        public List<IMixResult> Mix(IElement element, List<ITile> alreadyMixedTiles = null)
        {
            alreadyMixedTiles ??= new List<ITile>();

            if (alreadyMixedTiles.Contains(this))
                return null;

            alreadyMixedTiles.Add(this);

            List<IMixResult> mixResults = _mixOnSelf(element);

            if (mixResults.Exists(m => m.Success) && !element.CanStack)
                Element.Clear();

            foreach (IMixResult mixResult in mixResults)
            {
                if (mixResult.Success)
                {
                    Element.Add(mixResult.NewElement);
                }
            }

            if (element.CanSpread)
            {
                List<IMixResult> adjacentAffectedTiles = _tryMixOnAdjacent(element, alreadyMixedTiles);

                mixResults.AddRange(adjacentAffectedTiles);
            }

            return mixResults;
        }

        private List<IMixResult> _mixOnSelf(IElement elementToMix)
        {
            var mixResults = new List<IMixResult>();

            foreach (IElement elementOnTile in Element)
            {
                IElement mixedElement = elementOnTile.GetMixedElement(elementToMix);

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
            IElement element,
            List<ITile> alreadyMixedTiles)
        {
            var allResults = new List<IMixResult>();

            foreach (ITile tile in _adjacentTiles)
            {
                if (alreadyMixedTiles.Contains(tile))
                    continue;

                List<IMixResult> mixResults = tile.Mix(element, alreadyMixedTiles);

                if (mixResults != null)
                    allResults.AddRange(mixResults);

                alreadyMixedTiles.Add(tile);
            }

            return allResults;
        }
    }
}
