using System;
using Library;

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
            Console.WriteLine("{0} - {1}", "Title", e.Response.Data.TitleEncodedFancy);
            Console.WriteLine("{0} - {1}", "Tags", string.Join(", ", e.Response.Data.Tags));
            Console.WriteLine("{0} - {1}", "Last activity", e.Response.Data.LastActivityDate);
            Console.WriteLine("{0}", e.Response.Data.ApiSiteParameter);
            Console.WriteLine(e.Response.Data.QuestionUrl);
            Console.WriteLine();
        }
    }
}