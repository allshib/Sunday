using Sunday.Common.Interfaces;
using Sunday.Common.NonPersistent.Entities;
using Sunday.Common.NonPersistent.Events;
using Sunday.Common.NonPersistent.Types;
using Sunday.Common.SundayCommonDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunday.Common.NonPersistent.StateMachines
{
    public class StateMachine
    {

        private readonly IStateble _checkedObject;

        public StateMachine(IStateble checkedObject)
        {
            _checkedObject = checkedObject;
        }

        public FinalStateCheckResult GoToState(IState newState)
        {

            IState nextState = null;
            try
            {
                nextState = _checkedObject.SearchEndpointState(newState);
            }
            catch (Exception e)
            {
                return GetErrorFinalResult(e);
            }

            if (nextState == null)
            {
                var result = new FinalStateCheckResult(FinalStateCheckResultType.NotFoundEndpoitState);

                AllChecksFinished?.Invoke(_checkedObject, new FinalStateCheckResultEventArgs(result));

                return result;
            }


            FinalStateCheckResult finalResult = null;
            try
            {
                finalResult = _checkedObject.CheckTransTo(nextState);
            }
            catch (Exception e)
            {
                return GetErrorFinalResult(e);
            }


            if (finalResult.IsSuccess)
                _checkedObject.SetStateWithoutChecks(nextState);

            AllChecksFinished?.Invoke(_checkedObject, new FinalStateCheckResultEventArgs(finalResult));

            return finalResult;
        }

        private FinalStateCheckResult GetErrorFinalResult(Exception e)
        {
            var result = new FinalStateCheckResult
            {
                IsSuccess = false,
                CheckStatus = FinalStateCheckResultType.Error,
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            };

            AllChecksFinished?.Invoke(_checkedObject, new FinalStateCheckResultEventArgs(result));

            return result;
        }

        #region Events

        public delegate void StateCheckHandler(object sender, CheckFinishedEventArgs checkFinishedEventArgs);
        public delegate void AllStateCheckHandler(object sender, FinalStateCheckResultEventArgs finalStateCheckResult);


        public event StateCheckHandler FinishedCheck;
        public event AllStateCheckHandler AllChecksFinished;

        #endregion

    }
}
