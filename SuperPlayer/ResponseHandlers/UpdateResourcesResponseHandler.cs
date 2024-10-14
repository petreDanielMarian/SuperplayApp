using GameLogic.Types;
using Serilog;
using SuperPlayer.Interfaces;

namespace SuperPlayer.ResponseHandlers
{
    public class UpdateResourcesResponseHandler : IResponseHandler
    {
        public async Task HandleResponse(string response)
        {
            Log.Information($"UpdateResources response: {response}");

            string[] tokens = response.Split(" ");
            var resourceType = (PlayerResourceType)int.Parse(tokens[0]);
            int newBalance = int.Parse(tokens[1]);

            // Faulty update on server side handling
            if(resourceType == PlayerResourceType.None)
            {
                Console.WriteLine("Update failed");
            }
            else
            {
                Client.GetInstance.ActivePlayer.Resources[resourceType] = newBalance;
            }

            Console.WriteLine($"\nYour {resourceType} count is now {Client.GetInstance.ActivePlayer.Resources[resourceType]}");
            await Task.Delay(3000);
        }
    }
}
