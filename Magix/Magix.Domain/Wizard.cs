namespace Magix.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using Board;
    using NatureElements;

    public class Wizard
    {
        public int LifePoints { get; private set; }

        public int RemainingActions { get; private set; }

        public NatureElementEffect NatureElementEffect { get; private set; }

        public Position CurrentPosition { get; private set; }

        public Wizard(Position currentPosition)
        {
            NatureElementEffect = NatureElementEffect.None;
            CurrentPosition = currentPosition;
            LifePoints = 5;
            RemainingActions = 4;
        }

        public void Move(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
                tile.NatureElement.ApplyEffect(this);

            _removeRemainingActions(tiles.Count);

            CurrentPosition = tiles.Last().Position;
        }

        public void ApplyNatureElement(BaseNatureElement natureElement, List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
                tile.ApplyNatureElement(natureElement);

            _removeRemainingActions(tiles.Count);
        }

        public bool HasRemainingActions()
        {
            return RemainingActions == 0;
        }

        public void ChangeNatureElementEffect(NatureElementEffect natureElementEffect)
        {
            switch (natureElementEffect)
            {
                case NatureElementEffect.OnFire:
                    _takeDamage(1);
                    break;

                case NatureElementEffect.Blind:
                    _removeRemainingActions(1);
                    break;

                case NatureElementEffect.Shocked:
                    _removeRemainingActions(1);
                    break;
            }

            NatureElementEffect = natureElementEffect;
        }

        private void _takeDamage(int damage)
        {
            LifePoints -= damage;
        }

        private void _removeRemainingActions(int actionsRemoved)
        {
            RemainingActions -= actionsRemoved;
        }
    }
}
