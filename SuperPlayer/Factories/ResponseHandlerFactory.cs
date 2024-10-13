using GameLogic.Types;
using SuperPlayer.Interfaces;
using SuperPlayer.ResponseHandlers;
using System.Net.WebSockets;

namespace SuperPlayer.Factories
{
    public class ResponseHandlerFactory(WebSocket webSocket)
    {
        public IResponseHandler GetResponseHandler(CommandType responseType) => responseType switch
        {
            CommandType.Login => new LoginResponseHandler(),
            CommandType.UpdateResources => new UpdateResourcesResponseHandler(),
            CommandType.SendGift => new SendGiftResponseHandler(),
            CommandType.Exit => new ExitResponseHandler(webSocket),
            _ => throw new NotImplementedException("Response is not supported!"),
        };
    }
}
