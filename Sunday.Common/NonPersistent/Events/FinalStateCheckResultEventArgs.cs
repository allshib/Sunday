using System;
using System.Collections.Generic;
using System.Text;
using Sunday.Common.NonPersistent.Entities;

namespace Sunday.Common.NonPersistent.Events
{
    public class FinalStateCheckResultEventArgs : EventArgs
    {
        public FinalStateCheckResult FinalStateCheckResult { get; private set; }
        public FinalStateCheckResultEventArgs(FinalStateCheckResult finalStateCheckResult)
        {
            FinalStateCheckResult = finalStateCheckResult;
        }
    }
}
