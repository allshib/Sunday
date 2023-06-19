using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nominatim.Entities
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class Address
    {
        //public string street { get; set; }
        public string? city { get; set; }
        //public string city_district { get; set; }
        public string? country { get; set; }
        public string? state { get; set; }
        //public string suburb { get; set; }
        //public string country_code { get; set; }
        public string? house_number { get; set; }
        //public string neighbourhood { get; set; }
        public string? road { get; set; }
        public string? postcode { get; set; }
    }

    public class NominatimAdress
    {
        public long place_id { get; set; }
        public string ?licence { get; set; }
        public string ?osm_type { get; set; }
        public long osm_id { get; set; }
        public List<string> ?boundingbox { get; set; }
        public string? lat { get; set; }
        public string? lon { get; set; }
        public string ?display_name { get; set; }
        public long place_rank { get; set; }
        public string? category { get; set; }
        public string ?type { get; set; }
        public double? importance { get; set; }
        public string? icon { get; set; }
        public Address? address { get; set; }
    }
}
