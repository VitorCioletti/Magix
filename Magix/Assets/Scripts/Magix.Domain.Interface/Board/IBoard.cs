namespace Magix.Domain.Interface.Board
{
    using System;
    using System.Collections.Generic;
    using NatureElements;
    using Result;

    public interface IBoard
    {
        ITile[,] Tiles { get; }

        Queue<IPlayer> Players { get; }

        IPlayer CurrentPlayer { get; }

        Dictionary<Guid, Dictionary<IWizard, IPosition>> WizardsPositions { get; }

        IMovementResult Move(IWizard wizard, List<ITile> tiles);

        void ApplyNatureElement(IWizard wizard, INatureElement natureElement, List<ITile> tiles);

        List<IPosition> GetPreviewPositionMoves(IWizard wizard, ITile objectiveTile);

        IWizard GetWizard(ITile tile);
    }
}
