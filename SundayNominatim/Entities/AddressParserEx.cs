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
    public static class AddressParserEx
    {
        public static IAddress? ParseAddress(this NominatimAddress nominatimAddress, IAddress address)
        {

            if (nominatimAddress == null || address == null || String.IsNullOrEmpty(nominatimAddress.lat)) 
                return null;


            //double temp;
            address.Lat = Convert.ToDouble(nominatimAddress.lat, CultureInfo.InvariantCulture);
            address.Lon = Convert.ToDouble(nominatimAddress.lon, CultureInfo.InvariantCulture);

            address.Country = nominatimAddress?.address?.country;
            address.Region = nominatimAddress?.address?.state;
            address.Locality = nominatimAddress?.address?.city;
            address.Street = nominatimAddress?.address?.road;
            address.HomeNumber = nominatimAddress?.address?.house_number;
            address.DisplayName = nominatimAddress?.display_name;

            if (!String.IsNullOrEmpty(nominatimAddress?.address?.postcode))
                address.ZIP = Convert.ToInt32(nominatimAddress.address.postcode);

            return address;
        }

        public static IEnumerable<IAddress> ParseAddresses(this IEnumerable<NominatimAddress> nominatimAdresses, Func<IAddress> createAddressEntity)
        {
            var parsedAddressList = new List<IAddress>();

            foreach (var nominatimAddress in nominatimAdresses)
            {
                try
                {
                    var parsedAddress = nominatimAddress.ParseAddress(createAddressEntity.Invoke());

                    if (parsedAddress != null)
                        parsedAddressList.Add(parsedAddress);
                }
                catch {

                }
            }

            return parsedAddressList;
        }
    }
}
