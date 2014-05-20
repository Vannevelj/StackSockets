using System;
using System.Collections.Generic;
using Library.Utilities;
using Newtonsoft.Json;

namespace Library.ResponseMappers
{
    internal class Outer
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }

    internal class Inner
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

        [JsonProperty("lastActivityDate")]
        [JsonConverter(typeof (EpochTimeConverter))]
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