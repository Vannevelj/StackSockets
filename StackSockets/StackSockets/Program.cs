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
            Console.WriteLine("{0} - {1}", "Title", e.Response.TitleEncodedFancy);
            Console.WriteLine("{0} - {1}", "Tags", string.Join(", ", e.Response.Tags));
            Console.WriteLine("{0} - {1}", "Last activity", e.Response.LastActivityDate);
            Console.WriteLine("{0}", e.Response.ApiSiteParameter);
            Console.WriteLine(e.Response.QuestionUrl);
            Console.WriteLine();
        }
    }
}