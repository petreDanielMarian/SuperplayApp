using GameLogic.Types;
using SuperPlayer.Interfaces;
using SuperPlayer.ResponseHandlers;
using System.Net.WebSockets;

namespace SuperPlayer.Factories
{
    /// <summary>
    /// Factory class made to get the proper response handler
    /// </summary>
    /// <param name="webSocket"></param>
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
