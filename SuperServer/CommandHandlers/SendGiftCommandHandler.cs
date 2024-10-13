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
    public class SendGiftCommandHandler(WebSocket webSocket, string payload) : ICommandHandler
    {
        public async Task Handle()
        {
            string[] data = payload.Split(' ');
            var senderPlayerId = long.Parse(data[0]);
            var friednPlayerId = long.Parse(data[1]);
            var resourceType = (PlayerResourceType)int.Parse(data[2]);
            var amount = int.Parse(data[3]);

            try 
            { 
                Player friend = PlayerRepository.AddPlayerResources(friednPlayerId, resourceType, amount);
                Player sender = PlayerRepository.RemovePlayerResources(senderPlayerId, resourceType, amount);

                _ = Task.Run(() => NotifyActivePlayer(friend));

                await TransferDataHelper.SendTextOverChannelAsync(webSocket, new SendGiftResponse(friednPlayerId, (int)resourceType, sender.Resources[resourceType]).ToString());

            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Cannot update Player's {friednPlayerId} resources");

                await TransferDataHelper.SendTextOverChannelAsync(webSocket, new SendGiftResponse(senderPlayerId, (int)resourceType, amount).ToString());
            }
        
            await Task.CompletedTask;
        }

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
