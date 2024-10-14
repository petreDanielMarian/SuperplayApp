using GameLogic.Helpers;
using GameLogic.Messages.Notifications;
using GameLogic.Messages.Responses;
using GameLogic.Model;
using GameLogic.Types;
using SuperServer.Database;
using SuperServer.Interfaces;
using System.Net.WebSockets;

namespace SuperServer.CommandHandlers
{
    /// <summary>
    /// Handles SendGiftCommand and returns sender id if the update failed or reciever player id if the update succeeded, resource type and the amount
    /// </summary>
    /// <param name="webSocket"></param>
    /// <param name="payload">Contains sender player id, revceiver player id, resource type, gifted amount</param>
    public class SendGiftCommandHandler(WebSocket webSocket, string payload) : ICommandHandler
    {
        public async Task Handle()
        {
            string[] data = payload.Split(' ');
            var senderPlayerId = long.Parse(data[0]);
            var recieverPlayerId = long.Parse(data[1]);
            var resourceType = (PlayerResourceType)int.Parse(data[2]);
            var amount = int.Parse(data[3]);

            try 
            {
                Console.WriteLine($"Player {senderPlayerId} sends to player {recieverPlayerId} {amount} {resourceType}");

                Player friend = PlayerRepository.AddPlayerResources(recieverPlayerId, resourceType, amount);
                Player sender = PlayerRepository.RemovePlayerResources(senderPlayerId, resourceType, amount);

                _ = Task.Run(() => NotifyActivePlayer(friend));

                await TransferDataHelper.SendTextOverChannelAsync(webSocket, new SendGiftResponse(recieverPlayerId, (int)resourceType, sender.Resources[resourceType]).ToString());

            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Cannot update Player's {recieverPlayerId} resources");

                await TransferDataHelper.SendTextOverChannelAsync(webSocket, new SendGiftResponse(senderPlayerId, (int)resourceType, amount).ToString());
            }
        
            await Task.CompletedTask;
        }

        /// <summary>
        /// Task started if the gifted player is active
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private static async Task NotifyActivePlayer(Player player)
        {
            PlayerConnection? activePlayerConnection = PlayerRepository.GetActivePlayerByPlayerId(player.Id);
            if (activePlayerConnection != null)
            {
                await TransferDataHelper.SendTextOverChannelAsync(activePlayerConnection.WebSocket, new GiftRecievedNotification(player.Resources[PlayerResourceType.Coins], player.Resources[PlayerResourceType.Rolls]).ToString());
            }
        }
    }
}
