using GameLogic.Helpers;
using GameLogic.Messages.Responses;
using GameLogic.Model;
using GameLogic.Types;
using SuperServer.Database;
using SuperServer.Interfaces;
using System.Net.WebSockets;

namespace SuperServer.CommandHandlers
{
    public class UpdateResourcesCommandHandler : ICommandHandler
    {
        private WebSocket _webSocket;
        private string _payload;

        public UpdateResourcesCommandHandler(WebSocket webSocket, string payload)
        {
            _webSocket = webSocket;
            _payload = payload;
        }

        public async Task Handle()
        {
            string[] data = _payload.Split(' ');
            long playerId = long.Parse(data[0]);
            PlayerResourceType resourceType = (PlayerResourceType)int.Parse(data[1]);
            int amount = int.Parse(data[2]);

            try
            {
                Player sender = PlayerRepository.AddPlayerResources(playerId, resourceType, amount);

                await TransferDataHelper.SendTextOverChannel(_webSocket, new UpdateResourcesResponse(resourceType, sender.Resources[resourceType]).ToString());
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Cannot update {playerId} resources");
                await TransferDataHelper.SendTextOverChannel(_webSocket, new UpdateResourcesResponse(resourceType, 0).ToString());
            }
        }
    }
}
