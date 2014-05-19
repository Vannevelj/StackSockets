using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Library
{
    public class StackSocket
    {
        ClientWebSocket socket = new ClientWebSocket();
        private Uri uri;

        public event EventHandler<SocketEventArgs> OnSocketReceive;

        public StackSocket(string url)
        {
            uri = new Uri(url);
        }

        public async void Connect()
        {
            if (socket.State != WebSocketState.Open && socket.State != WebSocketState.Connecting)
            {
                await socket.ConnectAsync(uri, CancellationToken.None);
            }

            var request = Encoding.UTF8.GetBytes("155-questions-active");

            await socket.SendAsync(new ArraySegment<byte>(request), WebSocketMessageType.Text, true, CancellationToken.None);

            await Receive();
        }

        private async Task Receive()
        {
            var buffer = new byte[1024];

            while (socket.State == WebSocketState.Open)
            {
                var response = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (response.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close response received", CancellationToken.None);
                } else
                {
                    var result = Encoding.UTF8.GetString(buffer);
                    result = MakeJsonCapable(result);

                    OnSocketReceive.Invoke(this, new SocketEventArgs { Response = JsonConvert.DeserializeObject<Response>(result)});
                    buffer = new byte[1024];
                }
            }
        }

        private string MakeJsonCapable(string input)
        {
            input = input.Trim();
            input = input.TrimStart(new[] { '"' });
            input = input.Replace("data\":\"", "data\":");
            input = input.Remove(input.LastIndexOf("\""));
            input += "}";
            input = input.Replace("\\", string.Empty);

            return input;
        }
    }

    public class SocketEventArgs : EventArgs
    {
        public Response Response { get; set; }
    }
}
