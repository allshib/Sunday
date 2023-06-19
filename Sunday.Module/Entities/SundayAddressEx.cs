using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.BaseImpl;
using DevExpress.XtraSpreadsheet.DocumentFormats.Xlsb;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Nominatim.Entities;
using Sunday.Module.BusinessObjects.SundayDataModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using City = Sunday.Module.BusinessObjects.SundayDataModel.City;
using NomAdress = Nominatim.Entities.Address;

namespace Sunday.Module.Entities {
    public static class SundayAddressEx {

        public static void FillAddressByQuery(this AddressBase address, string query) {
            if (address == null) return;

            var nominatimAddress = WrapperNominatim.GetAdressData(query);

            if (nominatimAddress == null) return;

            if (String.IsNullOrEmpty(nominatimAddress.lat)) return;

            //double temp;
            address.Lat = Convert.ToDouble(nominatimAddress.lat, CultureInfo.InvariantCulture);
            address.Lon = Convert.ToDouble(nominatimAddress.lon, CultureInfo.InvariantCulture);

            //address.Lat = Double.TryParse(nominatimAddress.lat, out temp) ? temp : address.Lat;
            //address.Lon = Double.TryParse(nominatimAddress.lon, out temp) ? temp : address.Lon;

            SundayCountry country=null;
            if (!String.IsNullOrEmpty(nominatimAddress?.address?.country)) 
                country = FillCountry(address, nominatimAddress.address);
            
            SundayRegion region=null;
            if (!String.IsNullOrEmpty(nominatimAddress?.address?.state)) {
                region = FillRegion(address, nominatimAddress.address);
                if (country != null)
                    region.Country = address.Country;

            }

            Locality loc=null;
            if (!String.IsNullOrEmpty(nominatimAddress?.address?.city)) {
                loc = FillLoc(address, nominatimAddress.address);
                if (region != null) loc.PGUnit = region;
                else if (country != null) loc.PGUnit = address.Country;
            }

            if (!String.IsNullOrEmpty(nominatimAddress?.address?.road)) {
                var street = FillStreet(address, nominatimAddress.address);
                if (loc != null) street.Locacity = loc;
            }

            if (!String.IsNullOrEmpty(nominatimAddress.address.postcode))
                address.ZIP = Convert.ToInt32(nominatimAddress.address.postcode);

        }


        private static SundayRegion FillRegion(this AddressBase address, NomAdress nominatimAddress) {
            var region = address.Session.FindObject<SundayRegion>(CriteriaOperator.Parse("Name = ?", nominatimAddress.state));
            if (region == null) {
                region = new SundayRegion(address.Session);
                region.Name = nominatimAddress.state;
            }
            
            address.Region = region;
            return region;
        }



        private static SundayCountry FillCountry(this AddressBase address, NomAdress nominatimAddress) {
            var country = address.Session.FindObject<SundayCountry>(CriteriaOperator.Parse("Name = ?", nominatimAddress.country));
            if (country == null) {
                country = new SundayCountry(address.Session);
                country.Name = nominatimAddress.country;
            }
            address.Country = country;

            return country;
        }




        private static Locality FillLoc(this AddressBase address, NomAdress nominatimAddress) {
            var loc = address.Session.FindObject<Locality>(CriteriaOperator.Parse("Name = ?", nominatimAddress.city));
            if (loc == null) {
                loc = new City(address.Session);
                loc.Name = nominatimAddress.city;
            }
            address.Locality = loc;
            return loc;
        }


        private static SundayStreet FillStreet(this AddressBase address, NomAdress nominatimAddress) {
            var street = address.Session.FindObject<SundayStreet>(CriteriaOperator.Parse("Name = ?", nominatimAddress.road));
            if (street == null) {
                street = new SundayStreet(address.Session);
                street.Name = nominatimAddress.road;
                street.HouseNumber = nominatimAddress.house_number;
            }
            address.Street = street;

            return street;
        }
    }
}
