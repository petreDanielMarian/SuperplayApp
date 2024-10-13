using System.Net.WebSockets;
using System.Text;

namespace GameLogic.Helpers
{
    public static class TransferDataHelper
    {
        public async static Task SendTextOverChannel(WebSocket webSocket, string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async static Task<string> RecieveTextOverChannel(WebSocket webSocket)
        {
            byte[] buffer = new byte[1024];
            WebSocketReceiveResult webSocketReceiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            string response = Encoding.UTF8.GetString(buffer, 0, webSocketReceiveResult.Count);

            return response;
        }
    }
}
