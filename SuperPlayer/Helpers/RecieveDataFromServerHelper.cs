using GameLogic.Helpers;
using GameLogic.Types;
using SuperPlayer.Factories;
using SuperPlayer.Interfaces;
using SuperPlayer.ServerNotificationHandlers;
using System.Net.WebSockets;

namespace SuperPlayer.Helpers
{
    public static class RecieveDataFromServerHelper
    {
        /// <summary>
        /// Custom Client-side Recieve that waits either a 
        /// </summary>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        public async static Task RecieveServerMessageOverChannel(WebSocket webSocket)
        {
            string response = await TransferDataHelper.RecieveTextOverChannelAsync(webSocket);

            // get response handler
            string[] tokens = response.Split(" ");
            MessageType messageType = (MessageType)int.Parse(tokens[0]);

            IResponseHandler responseHandler;
            if (messageType == MessageType.ServerCommunication)
            {
                CommandType responseType = (CommandType)int.Parse(tokens[1]);
                response = string.Join(" ", tokens.Skip(2));
                responseHandler = new ResponseHandlerFactory(webSocket).GetResponseHandler(responseType);
            }
            else
            {
                response = string.Join(" ", tokens.Skip(1));
                responseHandler = new GiftRecievedNotificationHandler();
            }

            await responseHandler.HandleResponse(response);            
        }
    }
}
