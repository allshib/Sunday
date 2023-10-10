using Sunday.Common.BusinessObjects.NonPersistent;
using Sunday.Common.Interfaces;
using Sunday.Common.SundayCommonDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunday.Common.Entities
{
    public class StateMachine
    {
        private readonly IStateble _checkedObject;

        public IState CurrentState { get; set; }

        public StateMachine(IStateble checkedObject)
        {
            CurrentState = checkedObject.CurrentState;

            _checkedObject = checkedObject;
        }

        public FinalStateCheckResult ToState(IState newState)
        {
            var nextState = _checkedObject.SearchEndpointState(newState);

            if (nextState == null)
                throw new InvalidOperationException();

            var finalResult = _checkedObject.CheckTransTo(nextState);

            if (finalResult.ChecksResult)
                _checkedObject.SetStateWithoutChecks(nextState);

            
            return finalResult;
        }

    }
}
