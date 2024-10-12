using GameLogic.Types;
using SuperServer.CommandHandlers;
using SuperServer.Interfaces;
using System.Net.WebSockets;

namespace SuperServer.Factories
{
    internal class CommandHandlerFactory
    {
        private WebSocket _webSocket;
        private string _payload;

        public CommandHandlerFactory(WebSocket webSocket, string payload)
        {
            _webSocket = webSocket;
            _payload = payload;
        }

        public ICommandHandler GetCommandHandler(CommandType commandType) => commandType switch
        {
            CommandType.Login => new LoginCommandHandler(_webSocket, _payload),
            CommandType.UpdateResources => new UpdateResourcesCommandHandler(_webSocket, _payload),
            CommandType.SendGift => new SendGiftCommandHandler(_webSocket, _payload),
            CommandType.Exit => new ExitCommandHandler(_webSocket, _payload),
            _ => throw new NotImplementedException("More features to come! Stay tuned!"),
        };
    }
}
