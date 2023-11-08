using System;
using System.Collections.Generic;
using System.Text;
using Sunday.Common.NonPersistent.Entities;

namespace Sunday.Common.NonPersistent.Events
{
    public class CheckFinishedEventArgs : EventArgs
    {
        public FinalStateCheckResult FinalStateCheckResult { get; private set; }

        public CheckFinishedEventArgs(FinalStateCheckResult finalStateCheckResult)
        {
            FinalStateCheckResult = finalStateCheckResult;
        }
    }
}
