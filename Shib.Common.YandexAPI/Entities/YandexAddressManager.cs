using Shib.Common.Interfaces.Address;
using Shib.Common.YandexAPI.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Shib.Common.YandexAPI.Entities
{
    public class YandexAddressManager : IAddressSearcher
    {
        public string Name => "YANDEX";
        private readonly GeocoderClient geocoderClient;
       // private readonly GeosudgestClient geosudgestClient;
        private readonly Func<IAddress> defaultAddressCreator;

        

        public YandexAddressManager(GeocoderClient geocoderClient,Func<IAddress> defaultAddressCreator) {
            this.geocoderClient = geocoderClient;
            //this.geosudgestClient = geosudgestClient;
            this.defaultAddressCreator = defaultAddressCreator;
        }

        public IAddress? GetAddress(string query)
        {
            var res = geocoderClient.GetAddressByQuery(query).Result;

            var geoObject = res?.response?.GeoObjectCollection?.featureMember?.FirstOrDefault()?.GeoObject;


            return FillAddress(geoObject);
        }

        private IAddress? FillAddress(GeoObject? geoObject)
        {
            var myAddress = defaultAddressCreator.Invoke();

            var point = geoObject?.Point?.pos.Split(" ");

            myAddress.Lon = Convert.ToDouble(point[0], CultureInfo.InvariantCulture);
            myAddress.Lat = Convert.ToDouble(point[1], CultureInfo.InvariantCulture);

            var yandexAddress = geoObject?.metaDataProperty?.GeocoderMetaData?.Address;

            if (yandexAddress == null)
                return null;
 

            myAddress.DisplayName = yandexAddress?.formatted;
            myAddress.Country = yandexAddress?.Components?.FirstOrDefault(x => x.kind == "country")?.name;
            myAddress.Locality = yandexAddress?.Components?.FirstOrDefault(x => x.kind == "locality")?.name;
            myAddress.Locality = yandexAddress?.Components?.FirstOrDefault(x => x.kind == "locality")?.name;
            myAddress.Street = yandexAddress?.Components?.FirstOrDefault(x => x.kind == "street")?.name;
            myAddress.HomeNumber = yandexAddress?.Components?.FirstOrDefault(x => x.kind == "house")?.name;

            myAddress.Region = yandexAddress?.Components?
                .Where(x => x.kind == "province")
                .Select(x=>x.name)
                .DefaultIfEmpty("")
                .Min();

            myAddress.ZIP = Convert.ToInt32(yandexAddress?.postal_code, CultureInfo.InvariantCulture);

            return myAddress;
        }

        public IEnumerable<IAddress>? GetAddressList(string query)
        {
            var res = geocoderClient.GetAddressByQuery(query).Result;

            if (res?.response?.GeoObjectCollection?.featureMember == null)
                return Enumerable.Empty<IAddress>();

            var addressList = new List<IAddress>();
            foreach(var geoObject in res.response.GeoObjectCollection.featureMember.Select(x => x.GeoObject))
            {
                try
                {
                    addressList.Add(FillAddress(geoObject));
                }
                catch
                {

                }
            }
                           
            return addressList;
        }
    }
}
