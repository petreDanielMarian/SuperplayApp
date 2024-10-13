using GameLogic.Helpers;
using GameLogic.Messages.Responses;
using GameLogic.Model;
using GameLogic.Types;
using SuperServer.Database;
using SuperServer.Interfaces;
using System.Net.WebSockets;

namespace SuperServer.CommandHandlers
{
    public class UpdateResourcesCommandHandler(WebSocket webSocket, string payload) : ICommandHandler
    {
        public async Task Handle()
        {
            string[] data = payload.Split(' ');
            long playerId = long.Parse(data[0]);
            PlayerResourceType resourceType = (PlayerResourceType)int.Parse(data[1]);
            int amount = int.Parse(data[2]);

            try
            {
                Player sender = PlayerRepository.AddPlayerResources(playerId, resourceType, amount);

                await TransferDataHelper.SendTextOverChannelAsync(webSocket, new UpdateResourcesResponse((int)resourceType, sender.Resources[resourceType]).ToString());
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Cannot update {playerId} resources");

                await TransferDataHelper.SendTextOverChannelAsync(webSocket, new UpdateResourcesResponse((int)resourceType, 0).ToString());
            }
        }
    }
}
