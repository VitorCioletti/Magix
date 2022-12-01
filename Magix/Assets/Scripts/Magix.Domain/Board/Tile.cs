namespace Magix.Domain.Board
{
    using Interface.Board;
    using Interface.NatureElements;
    using NatureElements;

    public class Tile : ITile
    {
        public IPosition Position { get; private set; }

        public INatureElement BaseNatureElement { get; private set; }

        public Tile(BaseNatureElement baseNatureElement, Position position)
        {
            BaseNatureElement = baseNatureElement;
            Position = position;
        }

        public void ApplyNatureElement(INatureElement natureElement)
        {
            if (BaseNatureElement == null)
            {
                BaseNatureElement = natureElement;
                BaseNatureElement.ApplyNatureElement(null);
            }
            else
                BaseNatureElement.ApplyNatureElement(natureElement);
        }
    }
}
