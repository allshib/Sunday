using Sunday.Common.SundayCommonDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunday.Common.Entities
{
    public class StateMachine<T> where T : Enum
    {
        private readonly Func<State, State, object, bool> _transitFunc;

        private readonly object _checkedObject;

        public State CurrentState { get; set; }

        public StateMachine(Func<State, State, object, bool> transitFunc, object checkedObject, State currentState)
        {
            CurrentState = currentState;

            _transitFunc = transitFunc;
            _checkedObject = checkedObject;
        }

        public void ToState(T newState)
        {
            var nextState = CurrentState.OutStates?.SingleOrDefault(x => x.Code.Equals(newState));

            if (nextState == null)
                throw new InvalidOperationException();

            if (CheckTransit(nextState))
            {
                CurrentState = nextState;
            }


        }

        private bool CheckTransit(State newState)
            => _transitFunc(CurrentState, newState, _checkedObject);


    }
}
