using Shib.Common.Interfaces.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SundayNominatim.Entities
{
    public class ShibAddress : IAddress
    {
        public string DisplayName { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Locality { get; set; }
        public string Street { get; set ; }
        public string HomeNumber { get; set; }
        public int ZIP { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
