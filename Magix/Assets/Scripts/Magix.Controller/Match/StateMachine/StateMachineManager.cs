namespace Magix.Controller.Match.StateMachine
{
    using System.Collections.Generic;
    using Service.Interface;
    using States;

    public class StateMachineManager
    {
        private Stack<BaseState> _stateStack { get; set; }

        private readonly IMatchService _matchService;

        private readonly BoardController _boardController;

        public StateMachineManager(BoardController boardController, IMatchService matchService)
        {
            _boardController = boardController;
            _matchService = matchService;

            _stateStack = new Stack<BaseState>();

            _stateStack.Push(new EmptyState());
        }

        public BaseState GetCurrentState() => _stateStack.Peek();

        public void Push(BaseState state)
        {
            _stateStack.Push(state);

            state.Initialize(this, _boardController, _matchService);
        }

        public void Swap(BaseState state)
        {
            Pop();
            Push(state);
        }

        public void Pop()
        {
            GetCurrentState().Cleanup();

            _stateStack.Pop();
        }
    }
}
