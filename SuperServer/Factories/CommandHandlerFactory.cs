using GameLogic.Types;
using SuperServer.CommandHandlers;
using SuperServer.Interfaces;
using System.Net.WebSockets;

namespace SuperServer.Factories
{
    /// <summary>
    /// Factory class made to get the proper command handler
    /// </summary>
    /// <param name="webSocket"></param>
    public class CommandHandlerFactory(WebSocket webSocket, string payload, long clientId)
    {
        public ICommandHandler GetCommandHandler(CommandType commandType) => commandType switch
        {
            CommandType.Login => new LoginCommandHandler(webSocket, payload, clientId),
            CommandType.UpdateResources => new UpdateResourcesCommandHandler(webSocket, payload),
            CommandType.SendGift => new SendGiftCommandHandler(webSocket, payload),
            CommandType.Exit => new ExitCommandHandler(webSocket, payload, clientId),
            _ => throw new NotImplementedException("More features to come! Stay tuned!"),
        };
    }
}
