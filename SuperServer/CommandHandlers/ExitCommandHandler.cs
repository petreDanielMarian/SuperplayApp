using GameLogic.Helpers;
using Serilog;
using SuperServer.Database;
using SuperServer.Interfaces;
using SuperServer.Messages.Responses;
using System.Net.WebSockets;

namespace SuperServer.CommandHandlers
{
    /// <summary>
    /// When server sends Exit command, server sends and "OK" and proceeds to close the connection
    /// </summary>
    /// <param name="webSocket"></param>
    /// <param name="payload">Contains the client id</param>
    public class ExitCommandHandler(WebSocket webSocket, string payload, long clientId) : ICommandHandler
    {
        public async Task Handle()
        {
            try
            {
                await TransferDataHelper.SendTextOverChannelAsync(webSocket, new ExitResponse("OK").ToString());

                await Task.Delay(500);

                Log.Information($"Closing connection with client {clientId}");

                PlayerRepository.RemoveActivePlayer(long.Parse(payload));

                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Aggreed to stop channel", CancellationToken.None);
            }
            catch (Exception ex)
            {
                Log.Error("Exit command failed!" + ex.Message);
                throw;
            }
        }
    }
}
