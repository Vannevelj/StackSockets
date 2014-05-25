using System;
using System.Collections.Generic;
using Library.Requests;
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
        public abstract Activity Activity { get; }
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

        public override Activity Activity
        {
            get { return Activity.ActiveQuestions; }
        }
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

        public override Activity Activity
        {
            get { return Activity.NewestQuestionsByTag; }
        }
    }

    public abstract class QuestionActivityData : Data
    {
    }

    public sealed class CommentAddedData : QuestionActivityData
    {
        [JsonProperty("id")]
        public string PostId { get; internal set; }

        [JsonProperty("commentid")]
        public string CommentId { get; internal set; }

        [JsonProperty("acctid")]
        public string AccountId { get; internal set; }

        public override Activity Activity
        {
            get { return Activity.CommentAdd; }
        }
    }

    public sealed class PostEditedData : QuestionActivityData
    {
        [JsonProperty("id")]
        public string PostId { get; internal set; }

        [JsonProperty("acctid")]
        public string AccountId { get; internal set; }

        public override Activity Activity
        {
            get { return Activity.PostEdit; }
        }
    }

    public sealed class ScoreChangedData : QuestionActivityData
    {
        [JsonProperty("id")]
        public string PostId { get; internal set; }

        [JsonProperty("score")]
        public string Score { get; internal set; }

        public override Activity Activity
        {
            get { return Activity.ScoreChange; }
        }
    }

    public sealed class AnswerAddedData : QuestionActivityData
    {
        [JsonProperty("id")]
        public string PostId { get; internal set; }

        [JsonProperty("answerid")]
        public string AnswerId { get; internal set; }

        [JsonProperty("acctid")]
        public string AccountId { get; internal set; }

        public override Activity Activity
        {
            get { return Activity.AnswerAdd; }
        }
    }

    public sealed class AnswerAcceptedData : QuestionActivityData
    {
        [JsonProperty("id")]
        public string PostId { get; internal set; }

        [JsonProperty("answerid")]
        public string AnswerId { get; internal set; }

        [JsonProperty("acctid")]
        public string AccountId { get; internal set; }

        public override Activity Activity
        {
            get { return Activity.AnswerAccept; }
        }
    }

    public sealed class AnswerUnAcceptedData : QuestionActivityData
    {
        [JsonProperty("id")]
        public string PostId { get; internal set; }

        [JsonProperty("answerid")]
        public string AnswerId { get; internal set; }

        [JsonProperty("acctid")]
        public string AccountId { get; internal set; }

        public override Activity Activity
        {
            get { return Activity.AnswerUnaccept; }
        }
    }
}