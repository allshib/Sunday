using Sunday.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunday.Common.BusinessObjects.SundayCommonDataModelCode
{
    public interface IStateble
    {
        IState CurrentState { get; set; }

        void GoToState(IState state);
    }
}
