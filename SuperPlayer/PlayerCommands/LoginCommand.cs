using GameLogic.Extensions;
using GameLogic.Helpers;
using Serilog;
using SuperPlayer.Interfaces;
using SuperPlayer.Messages.Requests;
using System.Net.WebSockets;

namespace SuperPlayer.PlayerCommands
{
    public class LoginCommand() : IPlayerCommand
    {
        public async Task Execute(WebSocket webSocket)
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

        private string ComputePayloadData()
        {
            string input;
            int udid;

            do
            {
                Console.Write("Please enter your UDID (positive integer): ");
                input = ConsoleHelper.ReadLineSafelyFromConsole();
            } while (!int.TryParse(input, out udid));

            var request = new LoginRequest(udid).ToString();
            Log.Information(request);

            return request;
        }
    }
}
