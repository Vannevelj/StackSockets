using System;
using System.Collections;
using Library.Responses;
using Library.Utilities;
using Newtonsoft.Json;

namespace Library.Requests
{
    public abstract class RequestParameters
    {
        internal abstract string GetRequestValue();

        internal abstract JsonConverter ResponseDataType { get; }
    }

    public sealed class ActiveQuestionsRequestParameters : RequestParameters
    {
        public string SiteId { get; set; }

        internal override string GetRequestValue()
        {
            return SiteId + "-questions-active";
        }

        internal override JsonConverter ResponseDataType
        {
            get { return new DataConverter<ActiveQuestionsData>(); }
        }
    }

    public sealed class NewestQuestionsByTagRequestParameters : RequestParameters
    {
        public string SiteId { get; set; }
        public string Tag { get; set; }

        internal override string GetRequestValue()
        {
            return SiteId + "-questions-newest-tag-" + Tag.ToLower();
        }

        internal override JsonConverter ResponseDataType
        {
            get { return new DataConverter<NewestQuestionsByTagData>(); }
        }
    }

    public sealed class QuestionActivityRequestParameters : RequestParameters
    {
        public event EventHandler<SocketEventArgs> OnCommentAdded;
        public event EventHandler<SocketEventArgs> OnPostEdited;
        public event EventHandler<SocketEventArgs> OnScoreChange;

        public string SiteId { get; set; }
        public string QuestionId { get; set; }
        private Activity[] Activities { get; set; }

        internal override JsonConverter ResponseDataType
        {
            get { return new DataConverter<QuestionActivityData>(); }
        }

        internal override string GetRequestValue()
        {
            return SiteId + "-questions-newest-tag-" + QuestionId;
        }

        public void Subscribe(params Activity[] activities)
        {
            Activities = activities;
        }
    }
}