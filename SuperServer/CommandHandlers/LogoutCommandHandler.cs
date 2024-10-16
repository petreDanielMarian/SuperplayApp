using GameLogic.Helpers;
using Serilog;
using SuperServer.Database;
using SuperServer.Interfaces;
using SuperServer.Messages.Responses;
using System.Net.WebSockets;

namespace SuperServer.CommandHandlers
{
    internal class LogoutCommandHandler(WebSocket webSocket, long clientId) : ICommandHandler
    {
        public async Task Handle()
        {
            try
            {
                await TransferDataHelper.SendTextOverChannelAsync(webSocket, new LogoutResponse("OK").ToString());

                await Task.Delay(500);

                if (ClientIdUdidRepository.GetPlayerUdid(clientId, out long playerUdid))
                {
                    PlayerRepository.RemoveActivePlayer(playerUdid);
                    ClientIdUdidRepository.RemoveConnectionMapping(clientId);
                }

                Log.Information($"Logged out player with udid: {playerUdid} from client{clientId}");
            }
            catch (Exception ex)
            {
                Log.Error("Logout command failed!" + ex.Message);
                throw;
            }
        }
    }
}
