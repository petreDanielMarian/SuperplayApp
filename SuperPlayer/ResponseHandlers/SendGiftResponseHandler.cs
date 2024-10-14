using GameLogic.Types;
using Serilog;
using SuperPlayer.Interfaces;

namespace SuperPlayer.ResponseHandlers
{
    public class SendGiftResponseHandler : IResponseHandler
    {
        public async Task HandleResponse(string response)
        {
            Log.Information($"SendGift response: {response}");

            string[] tokens = response.Split(" ");
            long playerId = long.Parse(tokens[0]);
            PlayerResourceType resourceType = (PlayerResourceType)int.Parse(tokens[1]);
            int amount = int.Parse(tokens[2]);

            if (Client.GetInstance.ActivePlayer.Id == playerId)
            {
                Console.WriteLine($"\nThere is no player with the selected PlayerId. You have still {Client.GetInstance.ActivePlayer.Resources[resourceType]} {resourceType}");
                await Task.Delay(4000);
            }
            else
            {
                int remainingResources = Client.GetInstance.ActivePlayer.Resources[resourceType] = amount;
                Console.WriteLine($"\nYour friend {playerId} got your {resourceType} gift. You have now {remainingResources} {resourceType}");
                await Task.Delay(4000);
            }
        }
    }
}
