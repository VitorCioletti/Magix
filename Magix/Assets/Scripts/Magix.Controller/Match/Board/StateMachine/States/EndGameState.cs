namespace Magix.Controller.Match.Board.StateMachine.States
{
    using Domain.Interface;
    using Service.Interface;

    public class EndGameState : BaseState
    {
        private readonly IPlayer _winner;

        public EndGameState(IPlayer winner)
        {
            _winner = winner;
        }

        public override void Initialize(
            StateMachineManager stateMachineManager,
            BoardController boardController,
            IMatchService matchService)
        {
            base.Initialize(stateMachineManager, boardController, matchService);

            boardController.ShowEndGame(_winner);
        }

        public override void OnClickRestart()
        {
            MatchService.Restart();
            BoardController.Restart();

            Pop();
        }
    }
}
