namespace Magix.Domain.Board.Result
{
    using Interface;
    using Interface.Board.Result;

    public abstract class BaseResult : IResult
    {
        public IWizard Wizard { get; private set; }

        public bool Success { get; private set; }

        public string ErrorId { get; private set; }

        protected BaseResult(IWizard wizard, bool success, string errorId)
        {
            Success = success;
            ErrorId = errorId;
            Wizard = wizard;
        }
    }
}
