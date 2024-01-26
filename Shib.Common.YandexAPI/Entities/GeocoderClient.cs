using Shib.Common.YandexAPI.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
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

        public async Task<YandexGeocoderResult?> GetAddressByUri(string Uri)
        => await GetResult($"{uri}/?apikey={key}&format=json&uri={Uri}");

        public async Task<YandexGeocoderResult?> GetAddressByQuery(string query)
        => await GetResult($"{uri}/?apikey={key}&format=json&geocode={query}");

        private  Task<YandexGeocoderResult?> GetResult(string uri)
        {
            var msg = new HttpRequestMessage(HttpMethod.Get, uri);
            msg.Headers.Add("Accept-Language", "ru-ru");
            msg.Headers.Add("User-Agent", "Other");
            using var client = new HttpClient();

            var result = client.Send(msg);



            return SerrializeContent(result?.Content);
        }


        private async Task<YandexGeocoderResult?> SerrializeContent(HttpContent? content)
        {
            if (content == null) return null;

            DataContractJsonSerializer serrializer = new DataContractJsonSerializer(typeof(YandexGeocoderResult));
            try
            {
                var serrializedObject = serrializer.ReadObject(await content.ReadAsStreamAsync());
                return (YandexGeocoderResult?)serrializedObject;
            }
            catch (Exception exc)
            {
                throw new Exception("API геокодирования, Ошибка серриализации: " + exc.Message);
            }
        }



    }
}
