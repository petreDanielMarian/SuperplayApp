using GameLogic.Extensions;
using GameLogic.Helpers;
using SuperPlayer.Interfaces;
using System.Net.WebSockets;
using GameLogic.Messages.Requests;

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
            return new LoginRequestMessage(clientId).ToString();
        }
    }
}
