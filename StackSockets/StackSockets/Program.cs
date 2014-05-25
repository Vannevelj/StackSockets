using System;
using Library;
using Library.Requests;
using Library.Responses;

namespace Console
{
    //TODO: use proper types (like ints) for the JSON responses
    internal class Program
    {
        private static void Main(string[] args)
        {
            //ActiveQuestions();
            //NewestQuestionsByTag();
            QuestionActivity();

            System.Console.ReadKey();
        }

        #region ActiveQuestions

        private static void ActiveQuestions()
        {
            var settings = new ActiveQuestionsRequestParameters
            {
                SiteId = "155"
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
                return;
            }

            System.Console.WriteLine("{0} - {1}", "Title", data.TitleEncodedFancy);
            System.Console.WriteLine("{0} - {1}", "Tags", string.Join(", ", data.Tags));
            System.Console.WriteLine("{0} - {1}", "Last activity", data.LastActivityDate);
            System.Console.WriteLine("{0}", data.ApiSiteParameter);
            System.Console.WriteLine(data.QuestionUrl);
            System.Console.WriteLine();
        }

        #endregion

        #region NewestQuestionsByTag

        private static void NewestQuestionsByTag()
        {
            var settings = new NewestQuestionsByTagRequestParameters
            {
                SiteId = "1",
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
                return;
            }

            System.Console.WriteLine("{0} - {1}", "Title", data.Body);
            System.Console.WriteLine("{0} - {1}", "Tags", string.Join(", ", data.Tags));
            System.Console.WriteLine("{0} - {1}", "ID", data.PostId);
            System.Console.WriteLine("{0} - {1}", "Site ID", data.SiteId);
            System.Console.WriteLine(data.Fetch);
            System.Console.WriteLine();
        }

        #endregion

        #region QuestionActivity

        private static void QuestionActivity()
        {
            var settings = new QuestionActivityRequestParameters
            {
                SiteId = "1",
                QuestionId = "23855664"
            };

            //settings.Subscribe(Activity.CommentAdd, Activity.PostEdit, Activity.ScoreChange);

            settings.OnCommentAdded += OnQuestionActivityCommentAdded;
            settings.OnPostEdited += OnQuestionActivityPostEdited;
            settings.OnScoreChange += OnQuestionActivityScoreChanged;

            var socket = new StackSocket("wss://qa.sockets.stackexchange.com", settings);
            socket.Connect();
        }

        private static void OnQuestionActivityCommentAdded(object sender, SocketEventArgs e)
        {
            var data = e.Response.Data as CommentAddedData;
            if (data == null)
            {
                throw new Exception("commentadded");
            }

            System.Console.WriteLine("Comment Added");
            System.Console.WriteLine("{0} - {1}", "Post ID", data.PostId);
            System.Console.WriteLine("{0} - {1}", "Comment ID", data.CommentId);
            System.Console.WriteLine("{0} - {1}", "Account ID", data.AccountId);
            System.Console.WriteLine();
        }

        private static void OnQuestionActivityPostEdited(object sender, SocketEventArgs e)
        {
            var data = e.Response.Data as PostEditedData;
            if (data == null)
            {
                throw new Exception("postedited");
            }

            System.Console.WriteLine("Post Edited");
            System.Console.WriteLine("{0} - {1}", "Post ID", data.PostId);
            System.Console.WriteLine("{0} - {1}", "Account ID", data.AccountId);
            System.Console.WriteLine();
        }

        private static void OnQuestionActivityScoreChanged(object sender, SocketEventArgs e)
        {
            var data = e.Response.Data as ScoreChangedData;
            if (data == null)
            {
                throw new Exception("scorechanged");
            }

            System.Console.WriteLine("Score Changed");
            System.Console.WriteLine("{0} - {1}", "Post ID", data.PostId);
            System.Console.WriteLine("{0} - {1}", "Score", data.Score);
            System.Console.WriteLine();
        }

        #endregion
    }
}