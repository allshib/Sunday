using Shib.Common.YandexAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SundayTests
{
    public class YandexAPITest
    {

        [Fact]
        public void SimpleYandexTest()
        {
            var addressManager = new YandexAddressManager(
                new GeocoderClient("2e6c8a50-744d-48d7-832e-3fd44ce16701"), 
                new GeosudgestClient("d40fef97-cc1a-46cb-97a5-02556fcd0d52"));

            var result = addressManager.GetAddress("Пермь, Сортировочная 88");
        }

    }
}
