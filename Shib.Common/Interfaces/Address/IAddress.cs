using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shib.Common.Interfaces.Address
{
    public interface IAddress
    {
        public string? DisplayName { get; set; }


        public string? Country { get; set; }
        public string? Region { get; set; }
        /// <summary>
        /// Населенный пункт
        /// </summary>
        public string? Locality { get; set; }
        public string? Street { get; set; }
        public string? HomeNumber { get; set; }
        public int ZIP { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
