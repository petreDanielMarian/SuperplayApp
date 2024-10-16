using GameLogic.Helpers;
using GameLogic.Model;
using Serilog;
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
    public class LoginCommandHandler(WebSocket webSocket, string payload, long clientId) : ICommandHandler
    {
        public async Task Handle()
        {
            var udid = long.Parse(payload);
            long playerId = 0;
            Player? registeredPlayer = Player.EmptyPlayer;

            try
            {
                if (PlayerRepository.GetActivePlayerByUdid(udid) != null)
                {
                    Log.Information($"Player already logged in with UDID {udid}");
                }
                else
                {

                    if (PlayerRepository.GetAllRegisteredPlayers().TryGetValue(udid, out registeredPlayer))
                    {
                        Log.Information("Player registered... logging in!");
                    }
                    else
                    {
                        Log.Information($"Registering player {udid}");
                        playerId = RandomNumbersPool.GetUniquePlayerId();

                        registeredPlayer = new Player(playerId);
                        PlayerRepository.RegisterPlayer(udid, registeredPlayer);
                    }

                    ClientIdUdidRepository.AddConnectionMapping(clientId, udid);
                    PlayerRepository.MarkActivePlayer(udid, new PlayerConnection(registeredPlayer, webSocket));
                }

                await TransferDataHelper.SendTextOverChannelAsync(webSocket, new LoginResponse(registeredPlayer).ToString());
            }
            catch (Exception e)
            {
                Log.Error("Communication error!" + e.ToString());
            }
            
        }
    }
}
