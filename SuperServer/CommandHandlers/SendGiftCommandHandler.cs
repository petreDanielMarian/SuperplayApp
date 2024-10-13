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
    public class SendGiftCommandHandler : ICommandHandler
    {
        private WebSocket _webSocket;
        private string _payload;

        public SendGiftCommandHandler(WebSocket webSocket, string payload)
        {
            _webSocket = webSocket;
            _payload = payload;
        }

        public async Task Handle()
        {
            string[] data = _payload.Split(' ');
            var senderPlayerId = long.Parse(data[0]);
            var friednPlayerId = long.Parse(data[1]);
            var resourceType = (PlayerResourceType)int.Parse(data[2]);
            var amount = int.Parse(data[3]);

            try 
            { 
                Player friend = PlayerRepository.AddPlayerResources(friednPlayerId, resourceType, amount);
                Player sender = PlayerRepository.RemovePlayerResources(senderPlayerId, resourceType, amount);

                _ = Task.Run(() => NotifyActivePlayer(friend));

                await TransferDataHelper.SendTextOverChannel(_webSocket, new SendGiftResponse(friednPlayerId, resourceType, sender.Resources[resourceType]).ToString());

            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Cannot update Player's {friednPlayerId} resources");
                await TransferDataHelper.SendTextOverChannel(_webSocket, new SendGiftResponse(senderPlayerId, resourceType, amount).ToString());
            }
        
            await Task.CompletedTask;
        }

        private async Task NotifyActivePlayer(Player player)
        {
            PlayerConnection? activePlayerConnection = PlayerRepository.GetActivePlayerByPlayerId(player.Id);
            if (activePlayerConnection != null)
            {
                await TransferDataHelper.SendTextOverChannel(activePlayerConnection.WebSocket, new GiftRecievedNotification(player.Resources[PlayerResourceType.Coins], player.Resources[PlayerResourceType.Rolls]).ToString());
            }
        }
    }
}
