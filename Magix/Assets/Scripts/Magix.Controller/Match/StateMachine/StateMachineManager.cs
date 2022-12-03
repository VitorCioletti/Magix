namespace Magix.Controller.Match.StateMachine
{
    using System.Collections.Generic;
    using States;

    public class StateMachineManager
    {
        private Stack<BaseState> _stateStack { get; set; }

        public StateMachineManager()
        {
            _stateStack = new Stack<BaseState>();

            _stateStack.Push(new EmptyState());
        }

        public BaseState GetCurrentState() => _stateStack.Peek();

        public void Push(BaseState state)
        {
            _stateStack.Push(state);

            state.Initialize(this);
        }

        public void Swap(BaseState state)
        {
            Pop();
            Push(state);
        }

        public void Pop() => _stateStack.Pop();
    }
}
