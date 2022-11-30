namespace Magix.Domain.Board.Result
{
    using System.Collections.Generic;

    public class MovementResult : BaseResult
    {
        public string WizardId { get; private set; }

        public List<Tile> Moves { get; private set; }

        public MovementResult(
            List<Tile> moves,
            string wizardId,
            bool success,
            string errorId)
            : base(success, errorId)
        {
            Moves = moves;
            WizardId = wizardId;
        }
    }
}
