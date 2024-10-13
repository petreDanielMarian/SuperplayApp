using GameLogic.Helpers;
using GameLogic.Types;
using SuperPlayer.Interfaces;

namespace SuperPlayer.ServerNotificationHandlers
{
    public class GiftRecievedNotificationHandler : IResponseHandler
    {
        public async Task HandleResponse(string response)
        {
            string[] tokens = response.Split(" ");

            Client.GetInstance.ActivePlayer.Resources[PlayerResourceType.Coins] = int.Parse(tokens[0]);
            Client.GetInstance.ActivePlayer.Resources[PlayerResourceType.Rolls] = int.Parse(tokens[1]);

            await Task.Delay(1000);
            
            // In order to display a log, I am updating the view.
            ConsoleHelper.ExternalInput = $"You recieved {tokens[0]} Coins and {tokens[1]} Rolls.";
            ConsoleHelper.HasExternalInput = true;
        }
    }
}
