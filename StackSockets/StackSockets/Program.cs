using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

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
            Console.WriteLine(e.Response);
        }
    }
}
