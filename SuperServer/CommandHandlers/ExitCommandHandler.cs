using GameLogic.Helpers;
using GameLogic.Messages.Responses;
using SuperServer.Database;
using SuperServer.Interfaces;
using System.Net.WebSockets;

namespace SuperServer.CommandHandlers
{
    // TODO: Continue Docs
    // TODO: clean the code
    // TODO: make list of points to discuss
    // Todo: Add serilog
    // TODO: check corner-cases
    // TODO: Check if you can be with resource <0 when gifting
    // TODO: Try-catch everything and handle exceptions
    public class ExitCommandHandler(WebSocket webSocket, string payload) : ICommandHandler
    {
        public async Task Handle()
        {
            await TransferDataHelper.SendTextOverChannelAsync(webSocket, new ExitResponse("OK").ToString());

            await Task.Delay(1000);

            Console.WriteLine($"Closing connection with client {payload}");

            PlayerRepository.RemoveActivePlayer(long.Parse(payload));

            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Aggreed to stop channel", CancellationToken.None);
        }
    }
}
