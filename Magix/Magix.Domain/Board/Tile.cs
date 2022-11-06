namespace Magix.Domain.Board
{
    using NatureElements;

    public class Tile
    {
        public Position Position { get; private set; }

        public BaseNatureElement NatureElement { get; private set; }

        public Tile(BaseNatureElement natureElement, Position position)
        {
            NatureElement = natureElement;
            Position = position;
        }

        public void ApplyNatureElement(BaseNatureElement natureElement)
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
