using System;
using Library;
using Library.Responses;

namespace StackSockets
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var socket = new StackSocket("wss://qa.sockets.stackexchange.com");
            socket.OnSocketReceive += OnDataReceived;
            socket.Connect();

            Console.ReadKey();
        }

        private static void OnDataReceived(object sender, SocketEventArgs e)
        {
            var data = e.Response.Data as ActiveQuestionsData;
            if (data == null)
            {
                return;
            }

            Console.WriteLine("{0} - {1}", "Title", data.TitleEncodedFancy);
            Console.WriteLine("{0} - {1}", "Tags", string.Join(", ", data.Tags));
            Console.WriteLine("{0} - {1}", "Last activity", data.LastActivityDate);
            Console.WriteLine("{0}", data.ApiSiteParameter);
            Console.WriteLine(data.QuestionUrl);
            Console.WriteLine();
        }
    }
}