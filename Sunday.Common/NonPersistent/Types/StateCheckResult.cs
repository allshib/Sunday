using System;
using System.Collections.Generic;
using System.Text;

namespace Sunday.Common.NonPersistent.Types
{
    public struct StateCheckResult
    {
        public string CheckName { get; set; }
        public string CheckInfo { get; set; }
        public bool CheckResult { get; set; }
    }
}
