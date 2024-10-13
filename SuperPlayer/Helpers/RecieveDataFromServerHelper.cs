using GameLogic.Types;
using SuperPlayer.Factories;
using SuperPlayer.Interfaces;
using SuperPlayer.ServerNotificationHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace SuperPlayer.Helpers
{
    public static class RecieveDataFromServerHelper
    {
        public async static Task RecieveServerMessageOverChannel(WebSocket webSocket)
        {
            byte[] buffer = new byte[1024];
            WebSocketReceiveResult webSocketReceiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            string response = Encoding.UTF8.GetString(buffer, 0, webSocketReceiveResult.Count);

            // get response handler
            string[] tokens = response.Split(" ");
            MessageType messageType = (MessageType)int.Parse(tokens[0]);
            //Console.WriteLine($"DEBUG: Handling {messageType}");

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
