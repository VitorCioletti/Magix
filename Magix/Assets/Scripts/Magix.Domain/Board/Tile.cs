namespace Magix.Domain.Board
{
    using Interface.Board;
    using Interface.NatureElements;

    public class Tile : ITile
    {
        public IPosition Position { get; private set; }

        public INatureElement NatureElement { get; private set; }

        public Tile(INatureElement natureElement, Position position)
        {
            NatureElement = natureElement;
            Position = position;
        }

        public void ApplyNatureElement(INatureElement natureElement)
        {
            if (NatureElement == null)
            {
                NatureElement = natureElement;
                NatureElement.ApplyNatureElement(null);
            }
            else
                NatureElement.ApplyNatureElement(natureElement);
        }
    }
}
