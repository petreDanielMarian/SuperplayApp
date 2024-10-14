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

            // TODO: Not sure lock is really needed, have to check code
            lock (Client.GetInstance.ActivePlayer)
            {
                Client.GetInstance.ActivePlayer.Resources[PlayerResourceType.Coins] = int.Parse(tokens[0]);
                Client.GetInstance.ActivePlayer.Resources[PlayerResourceType.Rolls] = int.Parse(tokens[1]);
            }
            
            // Updating the view.
            ConsoleHelper.ExternalInput = $"You recieved {tokens[0]} Coins and {tokens[1]} Rolls.";
            await Task.Delay(1500);
            ConsoleHelper.HasExternalInput = true;
        }
    }
}
