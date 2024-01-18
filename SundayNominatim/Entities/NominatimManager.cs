
using System.Net;
using System.Runtime.Serialization.Json;

namespace Nominatim.Entities
{
    public class NominatimManager
    {
        private string _url = "http://nominatim.openstreetmap.org";

        public NominatimManager()
        {

        }

        public NominatimAdress? GetFirstAdressData(string? quary)
        => GetObjects<NominatimAdress>($"{_url}/search?q={quary}&format=jsonv2&limit=1&addressdetails=1")?.FirstOrDefault();

        public List<NominatimAdress>? GetListAdressData(string? quary, int limit = 10)
        => GetObjects<NominatimAdress>($"{_url}/search?q={quary}&format=jsonv2&limit={limit}&addressdetails=1");


        private List<T>? GetObjects<T>(string url)
        {
            var httpClient = new HttpClient();

            var msg = new HttpRequestMessage(HttpMethod.Get, url);
            msg.Headers.Add("Accept-Language", "ru-ru");
            msg.Headers.Add("User-Agent", "Other");


            HttpContent? content = null;
            try
            {
                var res = httpClient.Send(msg);

                if (res.StatusCode != HttpStatusCode.OK || res.Content == null)
                    throw new Exception("API геокодирования: Не удалось выполнить запрос");

                content = res.Content;
            }
            catch (WebException e)
            {
                HandleWebException(e);
            }

            return SerrializeContent(typeof(List<T>), content) as List<T>;
        }

        private object? SerrializeContent(Type type, HttpContent content)
        {
            DataContractJsonSerializer serrializer = new DataContractJsonSerializer(type);
            try
            {
                var serrializedObject = serrializer.ReadObject(content.ReadAsStream());
                return serrializedObject;
            }
            catch (Exception exc)
            {
                throw new Exception("API геокодирования, Ошибка серриализации: " + exc.Message);
            }
        }

        private void HandleWebException(WebException e)
        {
            if (e.Response == null)
                throw new Exception(e.Message);

            var code = (int)((HttpWebResponse)e.Response).StatusCode;

            switch (code)
            {
                case 400:
                    throw new Exception("API геокодирования: адрес не удалось найти");

                default:
                    throw new Exception("API геокодирования: " + e.Message);
            }
            
        }
    }
}
