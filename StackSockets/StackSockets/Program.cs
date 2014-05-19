using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Newtonsoft.Json;

namespace StackSockets
{
    class Program
    {
        static void Main(string[] args)
        {
            var socket = new StackSocket("wss://qa.sockets.stackexchange.com");
            socket.OnSocketReceive += OnDataReceived;
            socket.Connect();

            Console.ReadKey();
        }

        private static void OnDataReceived(object sender, SocketEventArgs e)
        {
            Console.WriteLine(string.Format("{0} - {1}", "Title", e.Response.Data.TitleEncodedFancy));
            Console.WriteLine(string.Format("{0} - {1}", "Tags", string.Join(", ", e.Response.Data.Tags)));
            Console.WriteLine(string.Format("{0} - {1}", "Last activity", e.Response.Data.LastActivityDate));
            Console.WriteLine();
        }
    }
}
