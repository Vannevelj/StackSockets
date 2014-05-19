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
        private static System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\test.txt", true);
        static void Main(string[] args)
        {
            var socket = new StackSocket("wss://qa.sockets.stackexchange.com");
            socket.OnSocketReceive += OnDataReceived;
            socket.Connect();

            Console.ReadKey();
            file.Close();
        }

        private static void OnDataReceived(object sender, SocketEventArgs e)
        { 
            Console.WriteLine(e.Response);

            file.WriteLine(e.Response);
        }
    }
}
