using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Library
{
    public class Response
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(Data);
        }
    }
}
