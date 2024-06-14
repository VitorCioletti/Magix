namespace Magix.Controller.Match.Board.StateMachine
{
    using System;
    using System.Collections.Generic;
    using Service.Interface;
    using States;
    using States.Result;

    public class StateMachineManager
    {
        private Stack<BaseState> _stateStack { get; set; }

        private readonly IMatchService _matchService;

        private readonly BoardController _boardController;

        private readonly Action<BaseState> _onChangeState;

        public StateMachineManager(
            BoardController boardController,
            IMatchService matchService,
            Action<BaseState> onChangeState)
        {
            _onChangeState = onChangeState;

            _boardController = boardController;
            _matchService = matchService;

            _stateStack = new Stack<BaseState>();

            _stateStack.Push(new EmptyState());
        }

        public BaseState GetCurrentState() => _stateStack.Peek();

        public void Push(BaseState state)
        {
            _stateStack.Push(state);

            _onChangeState?.Invoke(state);

            state.Initialize(this, _boardController, _matchService);
        }

        public void Swap(BaseState state)
        {
            Pop(GetCurrentState());
            Push(state);
        }

        public void Pop(BaseState state)
        {
            BaseState currentState = GetCurrentState();

            if (currentState != state)
            {
                throw new InvalidOperationException(
                    $"Current state \"{currentState}\" is different than state popping \"{state}\".");
            }

            BaseStateResult stateResult = currentState.Cleanup();

            _stateStack.Pop();

            _onChangeState?.Invoke(GetCurrentState());

            _stateStack.Peek().OnGotBackOnTop(stateResult);
        }
    }
}
