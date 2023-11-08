using System;
using System.Collections.Generic;
using System.Text;
using Sunday.Common.NonPersistent.Types;

namespace Sunday.Common.NonPersistent.Entities
{
    public class FinalStateCheckResult
    {
        public bool IsSuccess { get; set; }

        public List<StateCheckResult> StateCheckResults { get; set; } = new List<StateCheckResult>();

        public FinalStateCheckResultType CheckStatus { get; set; }

        public string ErrorMessage { get; set; }
        public string ErrorStackTrace { get; set; }


        public FinalStateCheckResult() { }
        public FinalStateCheckResult(FinalStateCheckResultType resultType)
        {
            CheckStatus = resultType;
            if (CheckStatus == FinalStateCheckResultType.Success)
                IsSuccess = true;
            else
                IsSuccess = false;

        }

    }
}
