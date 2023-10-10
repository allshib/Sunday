using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Sunday.Module.BusinessObjects.Interfaces;
using Sunday.Common.Interfaces;
using DevExpress.ExpressApp.StateMachine.NonPersistent;
using Sunday.Common.Entities;
using Sunday.Common.SundayCommonDataModel;
using State = Sunday.Common.SundayCommonDataModel.State;
using Sunday.Common.BusinessObjects.NonPersistent;
using static Sunday.Common.Interfaces.IStateble;
using DevExpress.CodeParser;
using static DevExpress.Map.OpenGL.OpenGLCheckingHelper;

namespace Sunday.Module.BusinessObjects.SundayDataModel {

	
	public  partial class Customer : IStateble
	{
		public Customer(Session session) : base(session) { }

        [Browsable(false)]
        public IState CurrentState => Status;

        public event StateCheckHandler CheckFinished;
        public event AllStateCheckHandler AllChecksFinished;

        public override void AfterConstruction() { base.AfterConstruction(); }

        public FinalStateCheckResult CheckTransTo(IState newState)
        {
            var finalResult = new FinalStateCheckResult();

            Check(finalResult);

            finalResult.ChecksResult = true;

            AllChecksFinished?.Invoke(finalResult);
            return finalResult;
            
        }
        private void Check(FinalStateCheckResult final)
        {
            //Симуляция проверок
            for(int i = 0;i<10;i++)
            {
                var checkResult = new StateCheckResult { CheckName = $"Проверка номер {i}", CheckResult = false };
                final.StateCheckResults.Add(checkResult);
                CheckFinished?.Invoke(checkResult);
            }
        }

        public void GoToState(IState state)
        {          
            var stateMachine = new StateMachine(this);

            stateMachine.ToState(state);

        }

        public IState SearchEndpointState(IState newState)
        {
            return Status.OutStates?.SingleOrDefault(x => x.StateCode.Equals(newState.StateCode));
        }

        public void SetStateWithoutChecks(IState newState)
        {
            if (newState is State newStatus)
                Status = newStatus;
        }
    }

}
