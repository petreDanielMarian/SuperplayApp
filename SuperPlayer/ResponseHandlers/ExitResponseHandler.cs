using SuperPlayer.Interfaces;
using System.Net.WebSockets;

namespace SuperPlayer.ResponseHandlers
{
    public class ExitResponseHandler(WebSocket webSocket) : IResponseHandler
    {
        public async Task HandleResponse(string response)
        {
            if (response.Equals("OK"))
            {
                Console.WriteLine("\nConnection with server will be closed now...");
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client wants to terminate the connection", CancellationToken.None);
            }

            await Task.Delay(2000);

            Environment.Exit(0);
        }
    }
}
