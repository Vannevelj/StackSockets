using Library;
using Library.Requests;
using Library.Responses;

namespace Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //ActiveQuestions();
            NewestQuestionsByTag();

            System.Console.ReadKey();
        }

        #region ActiveQuestions

        private static void ActiveQuestions()
        {
            var settings = new ActiveQuestionsRequestParameters
            {
                SiteId = "155"
            };
            var socket = new StackSocket("wss://qa.sockets.stackexchange.com", settings);
            socket.OnSocketReceive += OnActiveQuestionsDataReceived;
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
            var socket = new StackSocket("wss://qa.sockets.stackexchange.com", settings);
            socket.OnSocketReceive += OnNewestQuestionsByTagDataReceived;
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
            System.Console.WriteLine("{0} - {1}", "ID", data.Id);
            System.Console.WriteLine("{0} - {1}", "Site ID", data.SiteId);
            System.Console.WriteLine(data.Fetch);
            System.Console.WriteLine();
        }

        #endregion
    }
}