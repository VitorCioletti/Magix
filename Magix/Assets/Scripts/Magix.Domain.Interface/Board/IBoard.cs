namespace Magix.Domain.Interface.Board
{
    using System.Collections.Generic;
    using Element;
    using Result;

    public interface IBoard
    {
        ITile[,] Tiles { get; }

        List<IPlayer> Players { get; }

        IPlayer CurrentPlayer { get; }

        List<IElement> GetElementToCast();

        IMovementResult Move(IWizard wizard, IList<ITile> tiles);

        IAttackResult Attack(IWizard wizard, IPosition position);
        bool CanAttack(IWizard wizard);

        ICastResult CastElement(IWizard wizard, IElement element, IList<ITile> tiles);

        bool HasWizard(ITile tile);

        bool BelongsToCurrentPlayer(IWizard wizard);

        IList<ITile> GetPreviewPathTo(IWizard wizard, ITile objectiveTile);

        IList<ITile> GetPreviewArea(IWizard wizard, WizardActionType wizardAction);

        IWizard GetWizard(ITile tile);
    }
}
