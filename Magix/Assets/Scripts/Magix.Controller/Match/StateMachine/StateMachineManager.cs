namespace Magix.Controller.Match.StateMachine
{
    using System.Collections.Generic;
    using Service.Interface;
    using States;

    public class StateMachineManager
    {
        private Stack<BaseState> _stateStack { get; set; }

        private readonly IMatchService _matchService;

        public StateMachineManager(IMatchService matchService)
        {
            _matchService = matchService;

            _stateStack = new Stack<BaseState>();

            _stateStack.Push(new EmptyState());
        }

        public BaseState GetCurrentState() => _stateStack.Peek();

        public void Push(BaseState state)
        {
            _stateStack.Push(state);

            state.Initialize(this, _matchService);
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
