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

        public async Task AwaitConnectionsAsync()
        {
            while (true)
            {
                HttpListenerContext context = await HttpListener.GetContextAsync(); // This awaits a client to connect
                if (context.Request.IsWebSocketRequest)
                {
                    HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                    WebSocket webSocket = webSocketContext.WebSocket;

                    _ = Task.Run(() => HandleClientAsync(webSocket));
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

        private async Task HandleClientAsync(WebSocket webSocket)
        {
            // TODO: Handshake- AcceptConnection? make sure the client is our client (ValidateClientConnection)
            string clientInfo = await TransferDataHelper.RecieveTextOverChannelAsync(webSocket);
            Console.WriteLine($"Connection request recieved from client {clientInfo}");

            // TODO: Check client integrity (recieve clientId, and a number)
            await Task.Delay(1000);

            await TransferDataHelper.SendTextOverChannelAsync(webSocket, _serverId.ToString());

            Console.WriteLine($"Connection established with client {clientInfo}");
            // end handshake

            // start exchanging messages
            while (true)
            {
                string response = await TransferDataHelper.RecieveTextOverChannelAsync(webSocket);

                var commandType = (CommandType)int.Parse(response.Split()[0]);
                var payload = string.Join(" ", response.Split().Skip(1));

                ICommandHandler handler = new CommandHandlerFactory(webSocket, payload).GetCommandHandler(commandType);
                _ = Task.Run(handler.Handle);
            }
        }
    }
}
