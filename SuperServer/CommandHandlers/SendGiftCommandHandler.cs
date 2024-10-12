using SuperServer.Interfaces;
using System.Net.WebSockets;

namespace SuperServer.CommandHandlers
{
    internal class SendGiftCommandHandler : ICommandHandler
    {
        private WebSocket _webSocket;
        private string _payload;

        public SendGiftCommandHandler(WebSocket webSocket, string payload)
        {
            _webSocket = webSocket;
            _payload = payload;
        }

        public Task Handle()
        {
            throw new NotImplementedException();
        }
    }
}
