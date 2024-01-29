using DevExpress.Data.Filtering;
using Shib.Common.Interfaces.Address;
using Shib.XAF.Address.BusinessObjects.AddressModuleDataModel;

namespace Shib.XAF.Address.Entities {
    public static class AddressEx {

        public static void Clear(this AddressBase address)
        {
            address.Street = null;
            address.Region = null;
            address.Lat = 0;
            address.Lon = 0;
            address.Country = null;
            address.PostalCode = 0;
            address.Locality = null;
            address.FormattedAddress = null;
        }

        public static void FillAddressFromEntity(this AddressBase address, IAddress adressEntity) {
            if (address == null || adressEntity == null) return;

            address.Clear();

            address.Lat = adressEntity.Lat;
            address.Lon = adressEntity.Lon;

            Country country=null;
            if (!String.IsNullOrEmpty(adressEntity.Country)) 
                country = FillCountry(address, adressEntity);
            
            Region region=null;
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
                if (loc != null) street.Locality = loc;
            }

            if (adressEntity.ZIP > 0)
                address.PostalCode = adressEntity.ZIP;

            address.FormattedAddress = adressEntity.DisplayName;

        }


        private static Region FillRegion(this AddressBase address, IAddress adressEntity) {
            var region = address.Session.FindObject<Region>(CriteriaOperator.Parse("Name = ?", adressEntity.Region));
            if (region == null) {
                region = new Region(address.Session);
                region.Name = adressEntity.Region;
            }
            
            address.Region = region;
            return region;
        }



        private static Country FillCountry(this AddressBase address, IAddress adressEntity) {
            var country = address.Session.FindObject<Country>(CriteriaOperator.Parse("Name = ?", adressEntity.Country));
            if (country == null) {
                country = new Country(address.Session);
                country.Name = adressEntity.Country;
            }
            address.Country = country;

            return country;
        }




        private static Locality FillLoc(this AddressBase address, IAddress adressEntity) {
            var loc = address.Session.FindObject<Locality>(CriteriaOperator.Parse("Name = ?", adressEntity.Locality));
            if (loc == null) {
                loc = new Locality(address.Session);
                loc.Name = adressEntity.Locality;
            }
            address.Locality = loc;
            return loc;
        }


        private static Street FillStreet(this AddressBase address, IAddress adressEntity) {
            var street = address.Session.FindObject<Street>(CriteriaOperator.Parse("Name = ?", adressEntity.Street));
            if (street == null) {
                street = new Street(address.Session);
                street.Name = adressEntity.Street;
                street.HouseNumber = adressEntity.HomeNumber;
            }
            address.Street = street;

            return street;
        }
    }
}
