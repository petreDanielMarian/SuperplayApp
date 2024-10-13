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
        
        public static BlockingCollection<string> inputQueue = [];

        private static Client? _instance = null;
        public static Client GetInstance => _instance ??= new Client(8080);

        private Client(int port) 
        {
            WebSocket = new ClientWebSocket();
            ActivePlayer = Player.EmptyPlayer;
            _serverUri = new Uri($"ws://localhost:{port}");
            _clientId = RandomNumbersPool.GetUniqueClientId();
        }

        public async Task ConnectToServer()
        {
            try
            {
                await WebSocket.ConnectAsync(_serverUri, CancellationToken.None);

                await TransferDataHelper.SendTextOverChannel(WebSocket, _clientId.ToString());
                var serverInfo = await TransferDataHelper.RecieveTextOverChannel(WebSocket);

                Console.WriteLine($"Connected to the server {serverInfo}");
                await Task.Delay(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task HandleServerCommunication()
        {
            _ = Task.Run(() => HandleServerNotificaions(WebSocket));

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

        private async Task HandleServerNotificaions(WebSocket webSocket)
        {
            while (true)
            {
                //Console.WriteLine("DEBUG: Waiting for anything from server");
                await RecieveDataFromServerHelper.RecieveServerMessageOverChannel(webSocket);
            }
        }

        private CommandType GetCommandType()
        {
            string input = string.Empty;

            while (input == string.Empty || ToSupportedCommand(input) == CommandType.Retry)
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

            return ToSupportedCommand(input);
        }

        private CommandType ToSupportedCommand(string input) => input.ToLowerInvariant() switch
        {
            "l" => CommandType.Login,
            "u" => CommandType.UpdateResources,
            "s" => CommandType.SendGift,
            "e" => CommandType.Exit,
            _ => CommandType.Retry
        };

        private bool IsPlayerLoggedIn()
        {
            return ActivePlayer.Id > 0;
        }
    }
}
