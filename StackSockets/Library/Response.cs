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
            return JsonConvert.SerializeObject(this);
        }
    }

    public class Data
    {
        [JsonProperty("siteBaseHostAddress")]
        public string SiteBaseHostAddress { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("titleEncodedFancy")]
        public string TitleEncodedFancy { get; set; }

        [JsonProperty("bodySummary")]
        public string BodySummary { get; set; }

        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// Last moment of activity expressed in GMT.
        /// </summary>
        /// <returns></returns>
        [JsonProperty("lastActivityDate")]
        public DateTime LastActivityDate { get; set; }

        [JsonProperty("url")]
        public string QuestionUrl { get; set; }

        [JsonProperty("ownerUrl")]
        public string OwnerUrl { get; set; }

        [JsonProperty("ownerDisplayName")]
        public string OwnerDisplayName { get; set; }

        [JsonProperty("apiSiteParameter")]
        public string ApiSiteParameter { get; set; }
    }
}
