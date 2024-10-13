using System.Net.WebSockets;
using System.Text;

namespace GameLogic.Helpers
{
    public static class TransferDataHelper
    {
        /// <summary>
        /// Sends message over socket (can be used by both server and client)
        /// </summary>
        /// <param name="webSocket">The websocket through which the data is sent</param>
        /// <param name="message">The string message that should be send</param>
        /// <returns></returns>
        public async static Task SendTextOverChannelAsync(WebSocket webSocket, string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        /// <summary>
        /// Waits for a message over the socket
        /// </summary>
        /// <param name="webSocket">The socket we expect a message from</param>
        /// <returns>The byte[] response as string</returns>
        public async static Task<string> RecieveTextOverChannelAsync(WebSocket webSocket)
        {
            byte[] buffer = new byte[1024];
            WebSocketReceiveResult webSocketReceiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            string response = Encoding.UTF8.GetString(buffer, 0, webSocketReceiveResult.Count);

            return response;
        }
    }
}
