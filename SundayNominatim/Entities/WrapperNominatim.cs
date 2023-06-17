using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nominatim.Entities
{
    public static class WrapperNominatim
    {

        public static NominatimAdress GetAdressData(string quary)
        {
            return CreateRoutingManager().GetAdressData(quary);
        }


        private static NominatimManager CreateRoutingManager()
        {
            return new NominatimManager();
        }
    }
}
