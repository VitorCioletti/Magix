namespace Magix.Domain.Interface.Board
{
    using System.Collections.Generic;
    using NatureElements;
    using Result;

    public interface IBoard
    {
        ITile[,] Tiles { get; }

        Queue<IPlayer> Players { get; }

        IPlayer CurrentPlayer { get; }

        IMovementResult Move(IWizard wizar, List<ITile> tiles);

        void ApplyNatureElement(IWizard wizard, INatureElement natureElement, List<ITile> tiles);
    }
}
