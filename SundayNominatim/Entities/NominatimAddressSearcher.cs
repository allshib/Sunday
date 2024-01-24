using Shib.Common.Interfaces.Address;
using SundayNominatim.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nominatim.Entities
{
    public  class NominatimAddressSearcher : IAddressSearcher
    {
        private readonly NominatimClient httpManager = new NominatimClient();
        private readonly Func<IAddress> defaultAddressCreator;

        public NominatimAddressSearcher(Func<IAddress> defaultAddressCreator) {

            this.defaultAddressCreator = defaultAddressCreator;
        }

        public IAddress? GetAddress(string quary)
        {
            return httpManager.GetFirstAddressData(quary)?.ParseAddress(defaultAddressCreator.Invoke());
        }
        public IEnumerable<IAddress>? GetAddressList(string quary)
        {
            return httpManager.GetListAdressData(quary, 10)?.ParseAddresses(defaultAddressCreator);
        }
    }
}
