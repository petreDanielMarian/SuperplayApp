using GameLogic.Extensions;
using GameLogic.Helpers;
using GameLogic.Messages.Requests;
using GameLogic.Types;
using SuperPlayer.Interfaces;
using System.Net.WebSockets;

namespace SuperPlayer.PlayerCommands
{
    public class SendGiftCommand() : IPlayerCommand
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
                await Task.Delay(2000);
            }
        }

        private string ComputePayloadData()
        {
            long playerId;
            int amount;
            string input;
            PlayerResourceType resourceType;

            do
            {
                Console.Write("\nPlease type player's id (100-999): ");
                input = ConsoleHelper.ReadLineSafelyFromConsole();

            } while (!TryValidatePlayerId(input, out playerId));

            do
            {
                Console.Write("\nPlease specify the resource type, (C)oins or (R)olls: ");
                input = ConsoleHelper.ReadLineSafelyFromConsole();
                resourceType = input.ToPlayerResourceType();

            } while (resourceType == PlayerResourceType.None);

            do
            {
                Console.Write($"\nPlease enter the amount of {resourceType.ToString()} you wish to send.\nMust be lower than {Client.GetInstance.ActivePlayer.Resources[resourceType]}: ");
                input = ConsoleHelper.ReadLineSafelyFromConsole();

            } while (!TryValidateSentAmount(input, resourceType, out amount));

            return new SendGiftRequest(Client.GetInstance.ActivePlayer.Id, playerId, (int)resourceType, amount).ToString();
        }

        private bool TryValidatePlayerId(string input, out long playerId)
        {
            return long.TryParse(input, out playerId) && playerId > 99 && playerId < 1000 && playerId != Client.GetInstance.ActivePlayer.Id;
        }

        private bool TryValidateSentAmount(string input, PlayerResourceType resourceType, out int amount)
        {
            return int.TryParse(input, out amount) && amount <= Client.GetInstance.ActivePlayer.Resources[resourceType];
        }
    }
}
