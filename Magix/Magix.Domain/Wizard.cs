namespace Magix.Domain
{
    using System.Collections.Generic;
    using Board;
    using NatureElements;

    public class Wizard
    {
        public int LifePoints { get; private set; }

        public int RemainingActions { get; private set; }

        public NatureElementEffect NatureElementEffect { get; private set; }

        public Position Position { get; private set; }

        public Wizard(Position position)
        {
            NatureElementEffect = NatureElementEffect.None;
            Position = position;
            LifePoints = 5;
            RemainingActions = 4;
        }

        public void Move(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
                tile.NatureElement.ApplyEffect(this);

            _removeRemainingActions(tiles.Count);
        }

        public void ApplyNatureElement(BaseNatureElement natureElement, List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
                tile.ApplyNatureElement(natureElement);

            _removeRemainingActions(tiles.Count);
        }

        public void ChangeNatureElementEffect(NatureElementEffect natureElementEffect)
        {
            NatureElementEffect = natureElementEffect;

            switch (natureElementEffect)
            {
                case NatureElementEffect.OnFire:
                    _takeDamage(1);
                    break;

                case NatureElementEffect.Blind:
                    _removeRemainingActions(1);
                    break;
            }
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
