using GameLogic.Extensions;
using GameLogic.Helpers;
using GameLogic.Model;
using GameLogic.Types;
using SuperPlayer.Factories;
using SuperPlayer.Helpers;
using SuperPlayer.Interfaces;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace SuperPlayer
{
    public class Client
    {
        public ClientWebSocket WebSocket;
        public Player ActivePlayer;

        private readonly Uri _serverUri;
        private readonly long _clientId;

        private static Client? _instance = null;
        public static Client GetInstance => _instance ??= new Client(8080);

        private Client(int port) 
        {
            WebSocket = new ClientWebSocket();
            ActivePlayer = Player.EmptyPlayer;

            _serverUri = new Uri($"ws://localhost:{port}");
            _clientId = RandomNumbersPool.GetUniqueClientId();
        }

        public async Task ConnectToServerAsync()
        {
            try
            {
                await WebSocket.ConnectAsync(_serverUri, CancellationToken.None);

                await TransferDataHelper.SendTextOverChannelAsync(WebSocket, _clientId.ToString());
                var serverInfo = await TransferDataHelper.RecieveTextOverChannelAsync(WebSocket);

                Console.WriteLine($"Connected to the server {serverInfo}");
                await Task.Delay(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task HandleServerCommunicationAsync()
        {
            _ = Task.Run(() => HandleServerNotificaionsAsync(WebSocket));

            while (true)
            {
                try
                {
                    await Task.Delay(4000);
                    CommandType commandType = GetCommandType();
                    IPlayerCommand playerCommand = new PlayerCommandFactory(_clientId).GetCommand(commandType);

                    await playerCommand.Execute(WebSocket);
                }
                catch (WebSocketException webSocketException)
                {
                    Console.WriteLine($"Server shut down before finishing the close handshake: {webSocketException.Message}");
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        /// <summary>
        /// Ongoing listner for messages through the websocket
        /// </summary>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        private static async Task HandleServerNotificaionsAsync(WebSocket webSocket)
        {
            while (true)
            {
                await RecieveDataFromServerHelper.RecieveServerMessageOverChannel(webSocket);
            }
        }

        /// <summary>
        /// Choosing menu from which the user input dictates the command
        /// </summary>
        /// <returns>The chosed command as <see cref="CommandType"/></returns>
        private CommandType GetCommandType()
        {
            string input = string.Empty;

            while (input == string.Empty || input.ToSupportedCommand() == CommandType.Retry)
            {
                string alreadyLoggedIn = IsPlayerLoggedIn() ? " - Already logged in" : string.Empty;

                Console.Clear();

                Console.WriteLine($"Client app id is {_clientId}");

                if (IsPlayerLoggedIn())
                {
                    Console.WriteLine($"Player ID: {ActivePlayer.Id}");
                    Console.WriteLine($"Player's Coins: {ActivePlayer.Resources[PlayerResourceType.Coins]}");
                    Console.WriteLine($"Player's Rolls: {ActivePlayer.Resources[PlayerResourceType.Rolls]}");
                }

                Console.WriteLine($"\nChoose your next action.\n(L)ogin{alreadyLoggedIn}\n(U)pdate Resource\n(S)end Gift\n(E)xit\n");
                Console.Write("Type only the letter inside the paranthesis: ");
                input = ConsoleHelper.ReadFromConsoleExternal();
            }

            return input.ToSupportedCommand();
        }

        private bool IsPlayerLoggedIn() => ActivePlayer.Id > 0;
    }
}
