using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shib.Common.YandexAPI.Entities
{
    public class GeocoderClient
    {
        private readonly string uri = "https://geocode-maps.yandex.ru/1.x";

        private readonly string key;

        public GeocoderClient(string key)
        {
            this.key = key;
        }

        public async Task<string> GetResults(string q)
        {
            var response = new HttpRequestMessage(HttpMethod.Get, $"{uri}/?apikey={key}&format=json&uri={q}");
            using var client = new HttpClient();

            var result = await client.SendAsync(response);



            return await result.Content.ReadAsStringAsync();
        }



    }
}
