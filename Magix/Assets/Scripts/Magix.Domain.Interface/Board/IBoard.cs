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

        List<INatureElement> GetNatureElementsToCast();

        IMovementResult Move(IWizard wizard, IList<ITile> tiles);

        ICastResult CastNatureElement(IWizard wizard, INatureElement natureElement, IList<ITile> tiles);

        bool HasWizard(ITile tile);

        bool BelongsToCurrentPlayer(IWizard wizard);

        List<ITile> GetAreaToMove(IWizard wizard);

        List<ITile> GetPreviewPositionMoves(IWizard wizard, ITile objectiveTile);

        IWizard GetWizard(ITile tile);
    }
}
