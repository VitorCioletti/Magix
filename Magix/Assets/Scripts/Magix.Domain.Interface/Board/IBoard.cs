namespace Magix.Domain.Interface.Board
{
    using System.Collections.Generic;
    using NatureElements;
    using Result;

    public interface IBoard
    {
        ITile[,] Tiles { get; }

        List<IPlayer> Players { get; }

        IPlayer CurrentPlayer { get; }

        IMovementResult Move(IWizard wizard, List<ITile> tiles);

        IApplyNatureElementResult ApplyNatureElement(IWizard wizard, INatureElement natureElement, List<ITile> tiles);

        bool HasWizard(ITile tile);

        bool BelongsToCurrentPlayer(IWizard wizard);

        List<ITile> GetAreaToMove(IWizard wizard);

        List<ITile> GetPreviewPositionMoves(IWizard wizard, ITile objectiveTile);

        IWizard GetWizard(ITile tile);
    }
}
