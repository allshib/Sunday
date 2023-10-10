using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunday.Common.Interfaces
{
    public interface IState
    {
        string StateName { get; set; }

        int StateCode { get; set; }
    }
}
