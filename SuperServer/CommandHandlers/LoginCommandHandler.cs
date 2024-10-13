using GameLogic.Helpers;
using GameLogic.Messages.Responses;
using GameLogic.Model;
using SuperServer.Database;
using SuperServer.Interfaces;
using System.Net.WebSockets;

namespace SuperServer.CommandHandlers
{
    public class LoginCommandHandler(WebSocket webSocket, string payload) : ICommandHandler
    {
        public async Task Handle()
        {
            var udid = long.Parse(payload);
            long playerId;

            if (PlayerRepository.GetAllRegisteredPlayers().TryGetValue(udid, out Player? value))
            {
                playerId = -value.Id;
            }
            else
            {
                playerId = RandomNumbersPool.GetUniquePlayerId();
                PlayerRepository.RegisterPlayer(udid, new PlayerConnection(new Player(playerId), webSocket));
            }

            await TransferDataHelper.SendTextOverChannelAsync(webSocket, new LoginResponse(playerId).ToString());
        }
    }
}
