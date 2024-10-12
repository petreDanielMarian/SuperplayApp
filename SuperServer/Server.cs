using GameLogic.Helpers;
using GameLogic.Types;
using SuperServer.Factories;
using SuperServer.Interfaces;
using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace SuperServer
{
    public class Server
    { 
        private long _serverId { get; }
        public HttpListener HttpListener { get; init; }

        public Server()
        {
            _serverId = RandomNumbersPool.GetUniqueServerId();

            HttpListener = new HttpListener();
            HttpListener.Prefixes.Add("http://localhost:8080/");

            Console.WriteLine($"Server ({_serverId}) is running on {HttpListener.Prefixes.First()}");
        }

        public async Task AwaitConnections()
        {
            while (true)
            {
                HttpListenerContext context = await HttpListener.GetContextAsync(); // This awaits a client to connect
                if (context.Request.IsWebSocketRequest)
                {
                    HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                    WebSocket webSocket = webSocketContext.WebSocket;

                    _ = Task.Run(() => HandleClient(webSocket));
                }
            }
        }

        public void Start()
        {
            HttpListener.Start();
        }

        public void Shutdown()
        {
            HttpListener.Close();
        }

        private async Task HandleClient(WebSocket webSocket)
        {
            // todo Handshake? make sure the client is our client (ValidateClientConnection)
            byte[] clientInfoBuffer = new byte[50];
            WebSocketReceiveResult clientVerification = await webSocket.ReceiveAsync(new ArraySegment<byte>(clientInfoBuffer), CancellationToken.None);
            string clientInfo = Encoding.UTF8.GetString(clientInfoBuffer, 0, clientVerification.Count);
            Console.WriteLine($"Connection request recieved from client {clientInfo}");

            // todo Check client integrity (recieve clientId, and a number)
            await Task.Delay(1000);

            byte[] serverInfo = Encoding.UTF8.GetBytes(_serverId.ToString());
            await webSocket.SendAsync(new ArraySegment<byte>(serverInfo), WebSocketMessageType.Text, true, CancellationToken.None);

            Console.WriteLine($"Connection established with client {clientInfo}");
            // end handshake

            // start exchanging messages
            while (true)
            {
                byte[] buffer = new byte[1024];
                WebSocketReceiveResult webSocketReceiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                string response = Encoding.UTF8.GetString(buffer, 0, webSocketReceiveResult.Count);

                var commandType = (CommandType)int.Parse(response.Split()[0]);
                var payload = string.Join(" ", response.Split().Skip(1));

                ICommandHandler handler = new CommandHandlerFactory(webSocket, payload).GetCommandHandler(commandType);
                _ = Task.Run(handler.Handle);
            }
        }
    }
}
