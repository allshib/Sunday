using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shib.Common.Interfaces.Address
{
    public interface IAddressSearcher
    {
        IAddress? GetAddress(string query);

        IEnumerable<IAddress>? GetAddressList(string query);

        
    }
}
