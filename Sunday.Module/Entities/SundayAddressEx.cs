using DevExpress.Data.Filtering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Nominatim.Entities;
using Sunday.Module.BusinessObjects.SundayDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using City = Sunday.Module.BusinessObjects.SundayDataModel.City;

namespace Sunday.Module.Entities {
    public static class SundayAddressEx {

        public static void FillAddressByQuery(this AddressBase address, string query) {
            if (address == null) return;

            var nominatimAddress = WrapperNominatim.GetAdressData(query);

            if (nominatimAddress == null) return;

            if (String.IsNullOrEmpty(nominatimAddress.lat)) return;

            address.Lat = Convert.ToDouble(nominatimAddress.lat);
            address.Lon = Convert.ToDouble(nominatimAddress.lon);

            SundayCountry country=null;
            if (!String.IsNullOrEmpty(nominatimAddress?.address?.country)) {
                country = address.Session.FindObject<SundayCountry>(CriteriaOperator.Parse("Name = ?", nominatimAddress.address.country));
                if (country == null) {
                    country = new SundayCountry(address.Session);
                    country.Name = nominatimAddress.address.country;
                }
                address.Country = country;
            }
            SundayRegion region=null;
            if (!String.IsNullOrEmpty(nominatimAddress?.address?.state)) {
                region = address.Session.FindObject<SundayRegion>(CriteriaOperator.Parse("Name = ?", nominatimAddress.address.state));
                if (region == null) {
                    region = new SundayRegion(address.Session);
                    region.Name = nominatimAddress.address.state;
                }
                if(country != null) 
                    region.Country = address.Country;   

                address.Region = region;
            }

            Locality loc=null;
            if (!String.IsNullOrEmpty(nominatimAddress?.address?.city)) {
                loc = address.Session.FindObject<Locality>(CriteriaOperator.Parse("Name = ?", nominatimAddress.address.city));
                if (loc == null) {
                    loc = new City(address.Session);
                    loc.Name = nominatimAddress.address.city;
                }
                if (region != null) loc.PGUnit = region;
                else if(country !=null) loc.PGUnit = address.Country;
                address.Locality = loc;
            }

            if (!String.IsNullOrEmpty(nominatimAddress?.address?.road)) {
                var street = address.Session.FindObject<SundayStreet>(CriteriaOperator.Parse("Name = ?", nominatimAddress.address.road));
                if (street == null) {
                    street = new SundayStreet(address.Session);
                    street.Name = nominatimAddress.address.road;
                    street.HouseNumber = nominatimAddress.address.house_number;
                }
                if (loc != null) street.Locacity = loc;
                address.Street = street;
            }

            if(!String.IsNullOrEmpty(nominatimAddress.address.postcode))
                address.ZIP = Convert.ToInt32(nominatimAddress.address.postcode);

        }
    }
}
