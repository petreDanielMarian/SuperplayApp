using GameLogic.Helpers;
using GameLogic.Model;
using SuperServer.Database;
using SuperServer.Interfaces;
using SuperServer.Messages.Responses;
using System.Net.WebSockets;

namespace SuperServer.CommandHandlers
{
    /// <summary>
    /// When player sends the Login command, the server sends back a player id
    /// </summary>
    /// <param name="webSocket"></param>
    /// <param name="payload">Contains the client id</param>
    public class LoginCommandHandler(WebSocket webSocket, string payload) : ICommandHandler
    {
        public async Task Handle()
        {
            var udid = long.Parse(payload);
            long playerId;
            try
            {
                if (PlayerRepository.GetAllRegisteredPlayers().TryGetValue(udid, out Player? value))
                {
                    Console.WriteLine($"Registering client {udid}");
                    playerId = -value.Id;
                }
                else
                {
                    Console.WriteLine($"Logging client {udid}");
                    playerId = RandomNumbersPool.GetUniquePlayerId();
                    PlayerRepository.RegisterPlayer(udid, new PlayerConnection(new Player(playerId), webSocket));
                }

                await TransferDataHelper.SendTextOverChannelAsync(webSocket, new LoginResponse(playerId).ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Communication error!" + e.ToString());
            }
            
        }
    }
}
