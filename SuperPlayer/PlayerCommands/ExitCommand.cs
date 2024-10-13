using GameLogic.Extensions;
using GameLogic.Helpers;
using GameLogic.Types;
using SuperPlayer.Helpers;
using SuperPlayer.Interfaces;
using System.Net.WebSockets;

namespace SuperPlayer.PlayerCommands
{
    class ExitCommand : IPlayerCommand
    {
        private long _clientId;

        public ExitCommand(long clientId)
        {
            _clientId = clientId;
        }

        public async Task Execute(WebSocket webSocket)
        {
            var payload = ComputePayloadData();

            if (payload.IsNullOrEmpty())
            {
                Console.WriteLine("Payload data was not done correctly.");
                await Task.Delay(2000);
                return;
            }

            await TransferDataHelper.SendTextOverChannel(webSocket, payload);

            // await RecieveDataFromServerHelper.RecieveServerMessageOverChannel(webSocket);     
        }

        private string ComputePayloadData()
        {
            return $"{(int)CommandType.Exit} {_clientId}";
        }
    }
}
