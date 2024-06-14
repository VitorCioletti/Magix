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

        IList<ITile> GetPreviewPathTo(IWizard wizard, ITile objectiveTile);

        IList<ITile> GetPreviewArea(IWizard wizard);

        IWizard GetWizard(ITile tile);
    }
}
