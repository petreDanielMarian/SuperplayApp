using GameLogic.Helpers;
using SuperServer.Interfaces;
using System.Net.WebSockets;

namespace SuperServer.CommandHandlers
{
    internal class ExitCommandHandler : ICommandHandler
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
            await TransferDataHelper.SendTextOverChannel(_webSocket, "OK");

            await Task.Delay(1000);

            Console.WriteLine($"Closing connection with client {_payload}");

            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Aggreed to stop channel", CancellationToken.None);
        }
    }
}
