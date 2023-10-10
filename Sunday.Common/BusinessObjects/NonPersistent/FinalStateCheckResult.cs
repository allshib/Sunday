using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunday.Common.BusinessObjects.NonPersistent
{
    public class FinalStateCheckResult
    {
        public bool ChecksResult {  get; set; }

        public List<StateCheckResult> StateCheckResults { get; init; } =  new List<StateCheckResult>();


    }
}
