using Shib.Common.Interfaces.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Shib.Common.YandexAPI.Entities
{
    public class YandexAddressManager : IAddressSearcher
    {
        private readonly GeocoderClient geocoderClient;
        private readonly GeosudgestClient geosudgestClient;


        public YandexAddressManager(GeocoderClient geocoderClient, GeosudgestClient geosudgestClient) {
            this.geocoderClient = geocoderClient;
            this.geosudgestClient = geosudgestClient;
        }

        public IAddress? GetAddress(string query)
        {
            var address = geosudgestClient.GetResults(query).Result?.results?.FirstOrDefault();

            var res = geocoderClient.GetResults(address?.uri).Result;

            return null;
        }


        public IEnumerable<IAddress>? GetAddressList(string query)
        {
            throw new NotImplementedException();
        }
    }
}
