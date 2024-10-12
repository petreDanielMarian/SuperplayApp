using GameLogic.Extensions;
using GameLogic.Helpers;
using GameLogic.Types;
using SuperPlayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace SuperPlayer.PlayerCommands
{
    class ExitCommand : IPlayerCommand
    {
        private long _clientId;
        private string _payload = string.Empty;

        public ExitCommand(long clientId)
        {
            _clientId = clientId;
        }

        public async Task Execute(WebSocket webSocket)
        {
            if (_payload.IsNullOrEmpty())
            {
                Console.WriteLine("Payload data was not done correctly.");
                return;
            }

            await TransferDataHelper.SendTextOverChannel(webSocket, _payload);

            var response = await TransferDataHelper.RecieveTextOverChannel(webSocket);

            if (response.Equals("OK"))
            {
                Console.WriteLine("Connection with server will be closed now...");
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client wants to terminate the connection", CancellationToken.None);
            }

            await Task.Delay(3000);

            Environment.Exit(0);
        }

        public void ComputePayloadData()
        {
            _payload = $"{(int)CommandType.Exit} {_clientId}";
        }
    }
}
