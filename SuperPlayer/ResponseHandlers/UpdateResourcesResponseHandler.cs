using GameLogic.Types;
using SuperPlayer.Interfaces;

namespace SuperPlayer.ResponseHandlers
{
    public class UpdateResourcesResponseHandler : IResponseHandler
    {
        public async Task HandleResponse(string response)
        {
            string[] tokens = response.Split(" ");
            PlayerResourceType resourceType = (PlayerResourceType)int.Parse(tokens[0]);
            int amount = int.Parse(tokens[1]);

            if(resourceType == PlayerResourceType.None)
            {
                Console.WriteLine("Update failed");
            }
            else
            {
                Client.GetInstance.ActivePlayer.Resources[resourceType] = amount;
            }

            Console.WriteLine($"\nYour {resourceType} count is now {Client.GetInstance.ActivePlayer.Resources[resourceType]} ");
            await Task.Delay(3000);
        }
    }
}
