using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Utils;

using DevExpress.Persistent.BaseImpl;
using DevExpress.XtraSpreadsheet.DocumentFormats.Xlsb;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Nominatim.Entities;
using Shib.Common.Interfaces.Address;
using Shib.XAF.Address.BusinessObjects.NonPersistent;
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

        public static void Clear(this AddressBase address)
        {
            address.Street = null;
            address.Region = null;
            address.Lat = 0;
            address.Lon = 0;
            address.Country = null;
            address.ZIP = 0;
            address.Locality = null;
            address.FormattedAddress = null;
        }

        public static void FillAddressFromEntity(this AddressBase address, IAddress adressEntity) {
            if (address == null || adressEntity == null) return;

            address.Clear();

            address.Lat = adressEntity.Lat;
            address.Lon = adressEntity.Lon;

            SundayCountry country=null;
            if (!String.IsNullOrEmpty(adressEntity.Country)) 
                country = FillCountry(address, adressEntity);
            
            SundayRegion region=null;
            if (!String.IsNullOrEmpty(adressEntity.Region)) {
                region = FillRegion(address, adressEntity);
                if (country != null)
                    region.Country = address.Country;

            }

            Locality loc=null;
            if (!String.IsNullOrEmpty(adressEntity.Locality)) {
                loc = FillLoc(address, adressEntity);
                if (region != null) loc.PGUnit = region;
                else if (country != null) loc.PGUnit = address.Country;
            }

            if (!String.IsNullOrEmpty(adressEntity.Street)) {
                var street = FillStreet(address, adressEntity);
                if (loc != null) street.Locacity = loc;
            }

            if (adressEntity.ZIP > 0)
                address.ZIP = adressEntity.ZIP;

            address.FormattedAddress = adressEntity.DisplayName;

        }


        private static SundayRegion FillRegion(this AddressBase address, IAddress adressEntity) {
            var region = address.Session.FindObject<SundayRegion>(CriteriaOperator.Parse("Name = ?", adressEntity.Region));
            if (region == null) {
                region = new SundayRegion(address.Session);
                region.Name = adressEntity.Region;
            }
            
            address.Region = region;
            return region;
        }



        private static SundayCountry FillCountry(this AddressBase address, IAddress adressEntity) {
            var country = address.Session.FindObject<SundayCountry>(CriteriaOperator.Parse("Name = ?", adressEntity.Country));
            if (country == null) {
                country = new SundayCountry(address.Session);
                country.Name = adressEntity.Country;
            }
            address.Country = country;

            return country;
        }




        private static Locality FillLoc(this AddressBase address, IAddress adressEntity) {
            var loc = address.Session.FindObject<Locality>(CriteriaOperator.Parse("Name = ?", adressEntity.Locality));
            if (loc == null) {
                loc = new City(address.Session);
                loc.Name = adressEntity.Locality;
            }
            address.Locality = loc;
            return loc;
        }


        private static SundayStreet FillStreet(this AddressBase address, IAddress adressEntity) {
            var street = address.Session.FindObject<SundayStreet>(CriteriaOperator.Parse("Name = ?", adressEntity.Street));
            if (street == null) {
                street = new SundayStreet(address.Session);
                street.Name = adressEntity.Street;
                street.HouseNumber = adressEntity.HomeNumber;
            }
            address.Street = street;

            return street;
        }
    }
}
