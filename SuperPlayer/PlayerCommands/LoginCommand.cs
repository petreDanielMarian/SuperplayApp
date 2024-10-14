using GameLogic.Extensions;
using GameLogic.Helpers;
using Serilog;
using SuperPlayer.Interfaces;
using SuperPlayer.Messages.Requests;
using System.Net.WebSockets;

namespace SuperPlayer.PlayerCommands
{
    public class LoginCommand(long clientId) : IPlayerCommand
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
            var request = new LoginRequest(clientId).ToString();
            Log.Information(request);

            return request;
        }
    }
}
