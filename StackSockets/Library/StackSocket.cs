using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.ResponseMappers;
using Library.Responses;
using Newtonsoft.Json;

namespace Library
{
    public class StackSocket
    {
        static StackSocket()
        {
            Mapper.CreateMap<Inner, Response>();
            Mapper.CreateMap<Outer, Response>();
        }

        private readonly ClientWebSocket socket = new ClientWebSocket();
        private readonly Uri uri;

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

            await
                socket.SendAsync(new ArraySegment<byte>(request), WebSocketMessageType.Text, true,
                    CancellationToken.None);

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
                    await
                        socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close response received",
                            CancellationToken.None);
                }
                else
                {
                    var result = Encoding.UTF8.GetString(buffer);
                    var outer = JsonConvert.DeserializeObject<Outer>(result);
                    var inner = JsonConvert.DeserializeObject<Inner>(outer.Data);

                    var resp = Mapper.Map<Inner, Response>(inner);
                    resp = Mapper.Map(outer, resp);

                    OnSocketReceive.Invoke(this, new SocketEventArgs {Response = resp});
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