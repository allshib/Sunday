using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Nominatim.Entities
{
    public class NominatimManager
    {
        private string _url = "http://nominatim.openstreetmap.org";

        public NominatimManager()
        {

        }

        public NominatimAdress GetAdressData(string quary)
        {
            var t = GetObjects<NominatimAdress>($"{_url}/search?q={quary}&format=jsonv2&limit=1&addressdetails=1").FirstOrDefault();
            return t;
        }


        private List<T> GetObjects<T>(string url)
        {
            //var webr = WebRequest.Create(url);
            //webr.Headers.Add("User-Agent: Other");

            //HttpWebResponse resp = null;

            WebClient webClient = new WebClient();
            webClient.Headers.Add("Accept-Language: ru-ru");

            webClient.Headers.Add("User-Agent: Other");
            byte[] jsonData;


            try
            {
                jsonData = webClient.DownloadData(url);
                //resp = (HttpWebResponse)webr.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {

                    throw new UserFriendlyException(e.Message);
                }
                else
                {
                    var code = (int)((HttpWebResponse)e.Response).StatusCode;

                    switch (code)
                    {
                        case 400:
                            throw new UserFriendlyException("API геокодирования: адрес не удалось найти");

                        default:
                            throw new UserFriendlyException("API геокодирования: " + e.Message);
                    }
                }

            }
            //Stream stream = resp.GetResponseStream();
            //StreamReader sr = new StreamReader(stream, Encoding.GetEncoding(resp.CharacterSet));

            //string sReadData = sr.ReadToEnd();

            //StringReader re = new StringReader(sReadData);
            //var reader = new JsonTextReader(re);

            //var se = new JsonSerializer();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<T>));
            try
            {
                //var objects = se.Deserialize <List<T>> (reader);
                var objects = ser.ReadObject(new MemoryStream(jsonData));
                return objects as List<T>;
            }
            catch (Exception exc)
            {

                throw new UserFriendlyException("API геокодирования: " + exc.Message);
            }
        }
    }
}
