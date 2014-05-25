using System;
using System.Collections.Generic;
using Library.Utilities;
using Newtonsoft.Json;

namespace Library.Responses
{
    public sealed class Response
    {
        [JsonProperty("action")]
        public string Action { get; internal set; }

        [JsonProperty("data")]
        public Data Data { get; internal set; }
    }

    public abstract class Data
    {
    }

    public sealed class ActiveQuestionsData : Data
    {
        [JsonProperty("siteBaseHostAddress")]
        public string SiteBaseHostAddress { get; internal set; }

        [JsonProperty("id")]
        public string PostId { get; internal set; }

        [JsonProperty("titleEncodedFancy")]
        public string TitleEncodedFancy { get; internal set; }

        [JsonProperty("bodySummary")]
        public string BodySummary { get; internal set; }

        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; internal set; }

        [JsonProperty("lastActivityDate")]
        [JsonConverter(typeof (EpochTimeConverter))]
        public DateTime LastActivityDate { get; internal set; }

        [JsonProperty("url")]
        [JsonConverter(typeof (UriConverter))]
        public Uri QuestionUrl { get; internal set; }

        [JsonProperty("ownerUrl")]
        [JsonConverter(typeof (UriConverter))]
        public Uri OwnerUrl { get; internal set; }

        [JsonProperty("ownerDisplayName")]
        public string OwnerDisplayName { get; internal set; }

        [JsonProperty("apiSiteParameter")]
        public string ApiSiteParameter { get; internal set; }
    }

    public sealed class NewestQuestionsByTagData : Data
    {
        [JsonProperty("id")]
        public string PostId { get; internal set; }

        [JsonProperty("body")]
        public string Body { get; internal set; }

        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; internal set; }

        [JsonProperty("siteid")]
        public string SiteId { get; internal set; }

        [JsonProperty("fetch")]
        public bool Fetch { get; internal set; }
    }
}