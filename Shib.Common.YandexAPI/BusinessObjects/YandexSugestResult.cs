using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shib.Common.YandexAPI.BusinessObjects
{
    
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Address
    {
        public string formatted_address { get; set; }
        public List<Component> component { get; set; }
    }

    public class Component
    {
        public string name { get; set; }
        public List<string> kind { get; set; }
    }

    public class Distance
    {
        public double value { get; set; }
        public string text { get; set; }
    }

    public class Hl
    {
        public int begin { get; set; }
        public int end { get; set; }
    }

    public class Result
    {
        public Title title { get; set; }
        public Subtitle subtitle { get; set; }
        public List<string> tags { get; set; }
        public Distance distance { get; set; }
        public Address address { get; set; }
        public string uri { get; set; }
    }

    public class YandexSugestResult
    {
        public string suggest_reqid { get; set; }
        public List<Result> results { get; set; }
    }

    public class Subtitle
    {
        public string text { get; set; }
        public List<Hl> hl { get; set; }
    }

    public class Title
    {
        public string text { get; set; }
        public List<Hl> hl { get; set; }
    }
}
