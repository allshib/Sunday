using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunday.Common.BusinessObjects.NonPersistent
{
    public record StateCheckResult
    {
        public string CheckName { get; init; }
        public string CheckInfo { get; init; }
        public bool CheckResult { get; init; }
    }
}
