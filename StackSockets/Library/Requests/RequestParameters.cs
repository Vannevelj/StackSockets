using System;
using Library.Responses;
using Library.Utilities;
using Newtonsoft.Json;

namespace Library.Requests
{
    public abstract class RequestParameters
    {
        internal abstract string GetRequestValue();

        internal abstract JsonConverter ResponseDataType { get; }

        internal abstract void FireEvent(object sender, SocketEventArgs e);
    }

    public sealed class ActiveQuestionsRequestParameters : RequestParameters
    {
        public event EventHandler<SocketEventArgs> OnNewActivity;

        public string SiteId { get; set; }

        internal override string GetRequestValue()
        {
            return SiteId + "-questions-active";
        }

        internal override void FireEvent(object sender, SocketEventArgs e)
        {
            if (OnNewActivity != null)
            {
                OnNewActivity(sender, e);
            }
        }

        internal override JsonConverter ResponseDataType
        {
            get { return new DataConverter<ActiveQuestionsData>(); }
        }
    }

    public sealed class NewestQuestionsByTagRequestParameters : RequestParameters
    {
        public event EventHandler<SocketEventArgs> OnNewQuestion;

        public string SiteId { get; set; }
        public string Tag { get; set; }

        internal override string GetRequestValue()
        {
            return SiteId + "-questions-newest-tag-" + Tag.ToLower();
        }

        internal override void FireEvent(object sender, SocketEventArgs e)
        {
            if (OnNewQuestion != null)
            {
                OnNewQuestion(sender, e);
            }
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
        public event EventHandler<SocketEventArgs> OnAnswerAdded;
        public event EventHandler<SocketEventArgs> OnAnswerAccepted;
        public event EventHandler<SocketEventArgs> OnAnswerUnaccepted;

        public string SiteId { get; set; }
        public string QuestionId { get; set; }
        private Activity[] Activities { get; set; }

        internal override JsonConverter ResponseDataType
        {
            get { return new DataConverter<QuestionActivityData>(); }
        }

        internal override string GetRequestValue()
        {
            return SiteId + "-question-" + QuestionId;
        }

        public void Subscribe(params Activity[] activities)
        {
            Activities = activities;
        }

        internal override void FireEvent(object sender, SocketEventArgs e)
        {
            if (Activities == null || Array.Exists(Activities, x => x == e.Activity))
            {
                switch (e.Activity)
                {
                    case Activity.PostEdit:
                        if (OnPostEdited != null)
                        {
                            OnPostEdited(sender, e);
                        }
                        break;

                    case Activity.CommentAdd:
                        if (OnCommentAdded != null)
                        {
                            OnCommentAdded(sender, e);
                        }
                        break;

                    case Activity.AnswerAdd:
                        if (OnAnswerAdded != null)
                        {
                            OnAnswerAdded(sender, e);
                        }
                        break;

                    case Activity.ScoreChange:
                        if (OnScoreChange != null)
                        {
                            OnScoreChange(sender, e);
                        }
                        break;

                    case Activity.AnswerAccept:
                        if (OnAnswerAccepted != null)
                        {
                            OnAnswerAccepted(sender, e);
                        }
                        break;

                    case Activity.AnswerUnaccept:
                        if (OnAnswerUnaccepted != null)
                        {
                            OnAnswerUnaccepted(sender, e);
                        }
                        break;

                    default:
                        throw new ArgumentException("The passed activity is not applicable for this event.");
                }
            }
        }
    }
}