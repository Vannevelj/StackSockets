using System;
using Library;
using Library.Requests;
using Library.Responses;

namespace ConsoleApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //ActiveQuestions();
            //NewestQuestionsByTag();
            //QuestionActivity();
            ReviewActivity();

            Console.ReadKey();
        }

        #region ActiveQuestions

        private static void ActiveQuestions()
        {
            var settings = new ActiveQuestionsRequestParameters
            {
                SiteId = 155
            };

            settings.OnNewActivity += OnActiveQuestionsDataReceived;

            var socket = new StackSocket("wss://qa.sockets.stackexchange.com", settings);
            socket.Connect();
        }

        private static void OnActiveQuestionsDataReceived(object sender, SocketEventArgs e)
        {
            var data = e.Response.Data as ActiveQuestionsData;
            if (data == null)
            {
                throw new Exception("active questions");
            }

            Console.WriteLine("{0} - {1}", "Title", data.TitleEncodedFancy);
            Console.WriteLine("{0} - {1}", "Tags", string.Join(", ", data.Tags));
            Console.WriteLine("{0} - {1}", "Last activity", data.LastActivityDate);
            Console.WriteLine("{0} - {1}", "API site parameter", data.ApiSiteParameter);
            Console.WriteLine("{0} - {1}", "Question URL", data.QuestionUrl);
            Console.WriteLine();
        }

        #endregion

        #region NewestQuestionsByTag

        private static void NewestQuestionsByTag()
        {
            var settings = new NewestQuestionsByTagRequestParameters
            {
                SiteId = 1,
                Tag = "Java"
            };

            settings.OnNewQuestion += OnNewestQuestionsByTagDataReceived;

            var socket = new StackSocket("wss://qa.sockets.stackexchange.com", settings);
            socket.Connect();
        }

        private static void OnNewestQuestionsByTagDataReceived(object sender, SocketEventArgs e)
        {
            var data = e.Response.Data as NewestQuestionsByTagData;
            if (data == null)
            {
                throw new Exception("newest questions by tag");
            }

            Console.WriteLine("{0} - {1}", "Body", data.Body);
            Console.WriteLine("{0} - {1}", "Tags", string.Join(", ", data.Tags));
            Console.WriteLine("{0} - {1}", "ID", data.PostId);
            Console.WriteLine("{0} - {1}", "Site ID", data.SiteId);
            Console.WriteLine("{0} - {1}", "Fetch", data.Fetch);
            Console.WriteLine();
        }

        #endregion

        #region QuestionActivity

        private static void QuestionActivity()
        {
            var settings = new QuestionActivityRequestParameters
            {
                SiteId = 1,
                QuestionId = 23856972
            };

            settings.OnCommentAdded += OnQuestionActivityCommentAdded;
            settings.OnPostEdited += OnQuestionActivityPostEdited;
            settings.OnScoreChange += OnQuestionActivityScoreChanged;
            settings.OnAnswerAdded += OnQuestionActivityAnswerAdded;
            settings.OnAnswerAccepted += OnQuestionActivityAnswerAccepted;
            settings.OnAnswerUnaccepted += OnQuestionActivityAnswerUnaccepted;

            var socket = new StackSocket("wss://qa.sockets.stackexchange.com", settings);
            socket.Connect();
        }

        private static void OnQuestionActivityAnswerUnaccepted(object sender, SocketEventArgs e)
        {
            var data = e.Response.Data as AnswerUnAcceptedData;
            if (data == null)
            {
                throw new Exception("answerunaccepted");
            }

            Console.WriteLine("Answer Unaccepted");
            Console.WriteLine("{0} - {1}", "Post ID", data.PostId);
            Console.WriteLine("{0} - {1}", "Answer ID", data.AnswerId);
            Console.WriteLine("{0} - {1}", "Account ID", data.AccountId);
            Console.WriteLine();
        }

        private static void OnQuestionActivityAnswerAccepted(object sender, SocketEventArgs e)
        {
            var data = e.Response.Data as AnswerAcceptedData;
            if (data == null)
            {
                throw new Exception("answeraccepted");
            }

            Console.WriteLine("Answer Accepted");
            Console.WriteLine("{0} - {1}", "Post ID", data.PostId);
            Console.WriteLine("{0} - {1}", "Answer ID", data.AnswerId);
            Console.WriteLine("{0} - {1}", "Account ID", data.AccountId);
            Console.WriteLine();
        }

        private static void OnQuestionActivityAnswerAdded(object sender, SocketEventArgs e)
        {
            var data = e.Response.Data as AnswerAddedData;
            if (data == null)
            {
                throw new Exception("answeradded");
            }

            Console.WriteLine("Answer added");
            Console.WriteLine("{0} - {1}", "Post ID", data.PostId);
            Console.WriteLine("{0} - {1}", "Answer ID", data.AnswerId);
            Console.WriteLine("{0} - {1}", "Account ID", data.AccountId);
            Console.WriteLine();
        }

        private static void OnQuestionActivityCommentAdded(object sender, SocketEventArgs e)
        {
            var data = e.Response.Data as CommentAddedData;
            if (data == null)
            {
                throw new Exception("commentadded");
            }

            Console.WriteLine("Comment Added");
            Console.WriteLine("{0} - {1}", "Post ID", data.PostId);
            Console.WriteLine("{0} - {1}", "Comment ID", data.CommentId);
            Console.WriteLine("{0} - {1}", "Account ID", data.AccountId);
            Console.WriteLine();
        }

        private static void OnQuestionActivityPostEdited(object sender, SocketEventArgs e)
        {
            var data = e.Response.Data as PostEditedData;
            if (data == null)
            {
                throw new Exception("postedited");
            }

            Console.WriteLine("Post Edited");
            Console.WriteLine("{0} - {1}", "Post ID", data.PostId);
            Console.WriteLine("{0} - {1}", "Account ID", data.AccountId);
            Console.WriteLine();
        }

        private static void OnQuestionActivityScoreChanged(object sender, SocketEventArgs e)
        {
            var data = e.Response.Data as ScoreChangedData;
            if (data == null)
            {
                throw new Exception("scorechanged");
            }

            Console.WriteLine("Score Changed");
            Console.WriteLine("{0} - {1}", "Post ID", data.PostId);
            Console.WriteLine("{0} - {1}", "Score", data.Score);
            Console.WriteLine();
        }

        #endregion

        #region ReviewActivity

        private static void ReviewActivity()
        {
            var settings = new ReviewDashboardActivityRequestParameters
            {
                SiteId = 1
            };

            settings.OnReviewActivity += OnReviewActivityDataReceived;

            var socket = new StackSocket("wss://qa.sockets.stackexchange.com", settings);
            socket.Connect();
        }

        private static void OnReviewActivityDataReceived(object sender, SocketEventArgs e)
        {
            var data = e.Response.Data as ReviewDashboardActivityData;
            if (data == null)
            {
                throw new Exception("review activity");
            }

            Console.WriteLine("{0} - {1}", "User ID", data.UserId);
            Console.WriteLine("{0} - {1}", "Review Type", data.ReviewType);
            Console.WriteLine("{0} - {1}", "Body", data.HtmlBody);
            Console.WriteLine();
        }

        #endregion
    }
}