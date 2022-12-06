namespace Magix.Domain.Interface.Board.Result
{
    using System.Collections.Generic;

    public interface IApplyNatureElementResult : IResult
    {
        IWizard Wizard { get; }

        List<ITile> Tiles { get; }
    }
}
