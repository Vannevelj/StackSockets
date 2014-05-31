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
        public IData Data { get; internal set; }
    }

    public interface IData
    {
        Activity Activity { get; }
    }

    public sealed class ActiveQuestionsData : IData
    {
        [JsonProperty("siteBaseHostAddress")]
        public string SiteBaseHostAddress { get; internal set; }

        [JsonProperty("id")]
        public int PostId { get; internal set; }

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

        public Activity Activity
        {
            get { return Activity.ActiveQuestions; }
        }
    }

    public sealed class NewestQuestionsByTagData : IData
    {
        [JsonProperty("id")]
        public int PostId { get; internal set; }

        [JsonProperty("body")]
        public string Body { get; internal set; }

        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; internal set; }

        [JsonProperty("siteid")]
        public int SiteId { get; internal set; }

        [JsonProperty("fetch")]
        public bool Fetch { get; internal set; }

        public Activity Activity
        {
            get { return Activity.NewestQuestionsByTag; }
        }
    }

    public interface IQuestionActivityData : IData
    {
    }

    public sealed class CommentAddedData : IQuestionActivityData
    {
        [JsonProperty("id")]
        public int PostId { get; internal set; }

        [JsonProperty("commentid")]
        public int CommentId { get; internal set; }

        [JsonProperty("acctid")]
        public int AccountId { get; internal set; }

        public Activity Activity
        {
            get { return Activity.CommentAdd; }
        }
    }

    public sealed class PostEditedData : IQuestionActivityData
    {
        [JsonProperty("id")]
        public int PostId { get; internal set; }

        [JsonProperty("acctid")]
        public int AccountId { get; internal set; }

        public Activity Activity
        {
            get { return Activity.PostEdit; }
        }
    }

    public sealed class ScoreChangedData : IQuestionActivityData
    {
        [JsonProperty("id")]
        public int PostId { get; internal set; }

        [JsonProperty("score")]
        public int Score { get; internal set; }

        public Activity Activity
        {
            get { return Activity.ScoreChange; }
        }
    }

    public sealed class AnswerAddedData : IQuestionActivityData
    {
        [JsonProperty("id")]
        public int PostId { get; internal set; }

        [JsonProperty("answerid")]
        public int AnswerId { get; internal set; }

        [JsonProperty("acctid")]
        public int AccountId { get; internal set; }

        public Activity Activity
        {
            get { return Activity.AnswerAdd; }
        }
    }

    public sealed class AnswerAcceptedData : IQuestionActivityData
    {
        [JsonProperty("id")]
        public int PostId { get; internal set; }

        [JsonProperty("answerid")]
        public int AnswerId { get; internal set; }

        [JsonProperty("acctid")]
        public int AccountId { get; internal set; }

        public Activity Activity
        {
            get { return Activity.AnswerAccept; }
        }
    }

    public sealed class AnswerUnAcceptedData : IQuestionActivityData
    {
        [JsonProperty("id")]
        public int PostId { get; internal set; }

        [JsonProperty("answerid")]
        public int AnswerId { get; internal set; }

        [JsonProperty("acctid")]
        public int AccountId { get; internal set; }

        public Activity Activity
        {
            get { return Activity.AnswerUnaccept; }
        }
    }

    public sealed class ReviewDashboardActivityData : IData
    {
        [JsonProperty("i")]
        public ReviewQueue ReviewType { get; internal set; }

        [JsonProperty("u")]
        public int UserId { get; internal set; }

        [JsonProperty("html")]
        public string HtmlBody { get; internal set; }

        public Activity Activity
        {
            get { return Activity.DashboardActivity; }
        }
    }
}