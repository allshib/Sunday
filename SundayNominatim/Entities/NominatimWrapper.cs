using Shib.Common.Interfaces.Address;
using SundayNominatim.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nominatim.Entities
{
    public  class NominatimWrapper : IAddressSearcher
    {
        private readonly NominatimManager addressManager = new NominatimManager();
        private readonly Func<IAddress> addressCreator;


        public NominatimWrapper(Func<IAddress> createAddressEntity) {

            this.addressCreator = createAddressEntity;
        }

        public IAddress? GetAddress(string quary)
        {
            return addressManager.GetFirstAdressData(quary)?.ParseAddress(addressCreator.Invoke());
        }
        public IEnumerable<IAddress>? GetAddresses(string quary)
        {
            return addressManager.GetListAdressData(quary, 10)?.ParseAddresses(addressCreator);
        }
    }
}
