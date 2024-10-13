using GameLogic.Extensions;
using GameLogic.Helpers;
using GameLogic.Types;
using SuperPlayer.Interfaces;
using System.Net.WebSockets;

namespace SuperPlayer.PlayerCommands
{
    class ExitCommand(long clientId) : IPlayerCommand
    {
        public async Task Execute(WebSocket webSocket)
        {
            var payload = ComputePayloadData();

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
            return $"{(int)CommandType.Exit} {clientId}";
        }
    }
}
