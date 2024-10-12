using GameLogic.Helpers;
using GameLogic.Model;
using GameLogic.Types;
using SuperPlayer.Factories;
using SuperPlayer.Interfaces;
using System.Net.WebSockets;
using System.Text;

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

        public async Task ConnectToServer()
        {
            try
            {
                await WebSocket.ConnectAsync(_serverUri, CancellationToken.None);

                byte[] clientInfo = Encoding.UTF8.GetBytes(_clientId.ToString());
                await WebSocket.SendAsync(new ArraySegment<byte>(clientInfo), WebSocketMessageType.Text, true, CancellationToken.None);

                var serverInfoBuffer = new byte[100];
                var result = await WebSocket.ReceiveAsync(new ArraySegment<byte>(serverInfoBuffer), CancellationToken.None);
                string serverInfo = Encoding.UTF8.GetString(serverInfoBuffer, 0, result.Count);

                Console.WriteLine($"Connected to the server {serverInfo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task HandleServerCommunication()
        {
            while (true)
            {
                try
                {
                    CommandType commandType = GetCommandType();
                    IPlayerCommand playerCommand = new PlayerCommandFactory(_clientId).GetCommand(commandType);

                    playerCommand.ComputePayloadData();
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

        private CommandType GetCommandType()
        {
            string input = string.Empty;

            while (input == string.Empty || ToSupportedCommand(input) == CommandType.Retry)
            {
                string alreadyLoggedIn = IsPlayerLoggedIn() ? " - Already logged in" : string.Empty;

                Console.WriteLine($"\n\nChoose your next action.\n(L)ogin{alreadyLoggedIn}\n(U)pdate Resource\n(S)end Gift\n(E)xit\n");
                Console.Write("Type only the letter inside the paranthesis: ");
                input = ConsoleHelper.ReadLineSafelyFromConsole();
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
