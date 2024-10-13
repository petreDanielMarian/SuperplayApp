using GameLogic.Helpers;
using GameLogic.Messages.Responses;
using SuperServer.Database;
using SuperServer.Interfaces;
using System.Net.WebSockets;

namespace SuperServer.CommandHandlers
{
    public class ExitCommandHandler : ICommandHandler
    {
        private WebSocket _webSocket;
        private string _payload;

        public ExitCommandHandler(WebSocket webSocket, string payload)
        {
            _webSocket = webSocket;
            _payload = payload;
        }

        public async Task Handle()
        {
            await TransferDataHelper.SendTextOverChannel(_webSocket, new ExitResponse("OK").ToString());

            await Task.Delay(1000);

            Console.WriteLine($"Closing connection with client {_payload}");

            PlayerRepository.RemoveActivePlayer(long.Parse(_payload));

            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Aggreed to stop channel", CancellationToken.None);
        }
    }
}
