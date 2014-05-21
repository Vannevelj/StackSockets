using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Library.Requests;
using Library.Responses;
using Newtonsoft.Json;

namespace Library
{
    public sealed class StackSocket : IDisposable
    {
        private readonly ClientWebSocket _socket = new ClientWebSocket();
        private readonly Uri _uri;
        private readonly RequestParameters _requestParameters;
        private const int BufferSize = 4096;
        private const int BufferAmplifier = 20;

        public event EventHandler<SocketEventArgs> OnSocketReceive;

        public StackSocket(string url, RequestParameters parameters)
        {
            _uri = new Uri(url);
            _requestParameters = parameters;
        }

        public async void Connect()
        {
            if (_socket.State != WebSocketState.Open && _socket.State != WebSocketState.Connecting)
            {
                await _socket.ConnectAsync(_uri, CancellationToken.None);
            }

            var request = Encoding.UTF8.GetBytes(_requestParameters.GetRequestValue());

            await
                _socket.SendAsync(new ArraySegment<byte>(request), WebSocketMessageType.Text, true,
                    CancellationToken.None);

            await Receive();
        }

        private async Task Receive()
        {
            var temporaryBuffer = new byte[BufferSize];
            var buffer = new byte[BufferSize * BufferAmplifier];
            var offset = 0;

            while (_socket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult response;

                while (true)
                {
                    response =
                        await _socket.ReceiveAsync(new ArraySegment<byte>(temporaryBuffer), CancellationToken.None);
                    temporaryBuffer.CopyTo(buffer, offset);
                    offset += response.Count;
                    temporaryBuffer = new byte[BufferSize];

                    if (response.EndOfMessage)
                    {
                        break;
                    }
                }

                if (response.MessageType == WebSocketMessageType.Close)
                {
                    await
                        _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close response received",
                            CancellationToken.None);
                }
                else
                {
                    var result = Encoding.UTF8.GetString(buffer);
                    var responseObject = JsonConvert.DeserializeObject<Response>(result,
                        _requestParameters.ResponseDataType);

                    OnSocketReceive.Invoke(this, new SocketEventArgs {Response = responseObject});
                    buffer = new byte[BufferSize * BufferAmplifier];
                    offset = 0;
                }
            }
        }

        public void Dispose()
        {
            _socket.Dispose();
        }
    }

    public class SocketEventArgs : EventArgs
    {
        public Response Response { get; set; }
    }
}