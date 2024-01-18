using Nominatim.Entities;
using Shib.Common.Interfaces.Address;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SundayNominatim.Entities
{
    public static class NominatimParserEx
    {


        public static IAddress ParseAddress(this NominatimAdress nominatimAddress, IAddress address)
        {


            if (nominatimAddress == null || address == null) return null;


            if (String.IsNullOrEmpty(nominatimAddress.lat)) return null;

            //double temp;
            address.Lat = Convert.ToDouble(nominatimAddress.lat, CultureInfo.InvariantCulture);
            address.Lon = Convert.ToDouble(nominatimAddress.lon, CultureInfo.InvariantCulture);


            address.Country = nominatimAddress?.address?.country;

            address.Region = nominatimAddress?.address?.state;

            address.Locality = nominatimAddress?.address?.city;

            address.Street = nominatimAddress?.address?.road;

            address.HomeNumber = nominatimAddress?.address?.house_number;

            if (!String.IsNullOrEmpty(nominatimAddress?.address?.postcode))
                address.ZIP = Convert.ToInt32(nominatimAddress.address.postcode);

            return address;
        }

        public static IEnumerable<IAddress> ParseAddresses(this IEnumerable<NominatimAdress> adresses, Func<IAddress> createAddressEntity)
        {
            var addressesList = new List<IAddress>();

            foreach (var adress in adresses)
            {
                try
                {
                    addressesList.Add(ParseAddress(adress, createAddressEntity.Invoke()));
                }
                catch {

                }
            }

            return addressesList;
        }
    }
}
