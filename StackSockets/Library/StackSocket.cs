using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Library.Responses;
using Newtonsoft.Json;

namespace Library
{
    public sealed class StackSocket
    {
        private readonly ClientWebSocket _socket = new ClientWebSocket();
        private readonly Uri _uri;

        public event EventHandler<SocketEventArgs> OnSocketReceive;

        public StackSocket(string url)
        {
            _uri = new Uri(url);
        }

        public async void Connect()
        {
            if (_socket.State != WebSocketState.Open && _socket.State != WebSocketState.Connecting)
            {
                await _socket.ConnectAsync(_uri, CancellationToken.None);
            }

            var request = Encoding.UTF8.GetBytes("155-questions-active");

            await
                _socket.SendAsync(new ArraySegment<byte>(request), WebSocketMessageType.Text, true,
                    CancellationToken.None);

            await Receive();
        }

        private async Task Receive()
        {
            var buffer = new byte[1024];

            while (_socket.State == WebSocketState.Open)
            {
                var response = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (response.MessageType == WebSocketMessageType.Close)
                {
                    await
                        _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close response received",
                            CancellationToken.None);
                }
                else
                {
                    var result = Encoding.UTF8.GetString(buffer);
                    var outer = JsonConvert.DeserializeObject<Response>(result);

                    OnSocketReceive.Invoke(this, new SocketEventArgs {Response = outer});
                    buffer = new byte[1024];
                }
            }
        }
    }

    public class SocketEventArgs : EventArgs
    {
        public Response Response { get; set; }
    }
}