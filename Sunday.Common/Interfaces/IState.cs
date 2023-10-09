using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunday.Common.Interfaces
{
    public interface IState
    {
        string Name { get; set; }

        int Code { get; set; };
    }
}
