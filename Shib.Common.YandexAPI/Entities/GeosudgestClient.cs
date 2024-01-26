using Shib.Common.YandexAPI.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Shib.Common.YandexAPI.Entities
{
    public class GeosudgestClient
    {
        private readonly string uri = "https://suggest-maps.yandex.ru";

        private readonly string key;

        public GeosudgestClient(string key)
        {
            this.key = key;
        }

        public async Task<YandexSugestResult?> GetResults(string q)
        {
            var response = new HttpRequestMessage(HttpMethod.Get, $"{uri}/v1/suggest?apikey={key}&attrs=uri&print_address=1&text={q}");
            using var client = new HttpClient();

            HttpResponseMessage? result = null;
            try
            {
                result = await client.SendAsync(response);
            }
            catch (Exception ex) { }


            return await SerrializeContent(result?.Content);
        }

        private async Task<YandexSugestResult?> SerrializeContent(HttpContent? content)
        {
            if (content == null) return null;

            DataContractJsonSerializer serrializer = new DataContractJsonSerializer(typeof(YandexSugestResult));
            try
            {
                var serrializedObject = serrializer.ReadObject(await content.ReadAsStreamAsync());
                return (YandexSugestResult?)serrializedObject;
            }
            catch (Exception exc)
            {
                throw new Exception("API геокодирования, Ошибка серриализации: " + exc.Message);
            }
        }
    }
}
