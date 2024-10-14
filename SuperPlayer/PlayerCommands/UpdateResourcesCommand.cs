using GameLogic.Extensions;
using GameLogic.Helpers;
using GameLogic.Types;
using SuperPlayer.Interfaces;
using SuperPlayer.Messages.Requests;
using System.Net.WebSockets;

namespace SuperPlayer.PlayerCommands
{
    public class UpdateResourcesCommand(long clientId) : IPlayerCommand
    {
        public async Task Execute(WebSocket webSocket)
        {
            if (Client.GetInstance.ActivePlayer.Id > 0)
            {
                string payload = ComputePayloadData();

                if (payload.IsNullOrEmpty())
                {
                    Console.WriteLine("Payload data was not done correctly.");
                    await Task.Delay(2000);
                    return;
                }

                await TransferDataHelper.SendTextOverChannelAsync(webSocket, payload);
            }
            else
            {
                Console.WriteLine("You need to log in first!");
                await Task.Delay(3000);
            }
        }

        private string ComputePayloadData()
        {
            int amount;
            string input;
            PlayerResourceType resourceType;

            do
            {
                Console.Write("\nPlease specify the resource type, (C)oins or (R)olls: ");
                input = ConsoleHelper.ReadLineSafelyFromConsole();
                resourceType = input.ToPlayerResourceType();

            } while (resourceType == PlayerResourceType.None);

            do
            {
                Console.Write($"\nPlease enter the amount of {resourceType.ToString()} you wish to add: ");
                input = ConsoleHelper.ReadLineSafelyFromConsole();

            } while (!int.TryParse(input, out amount));

            return new UpdateResourcesRequest(clientId, Client.GetInstance.ActivePlayer.Id, (int)resourceType, amount).ToString();
        }
    }
}
