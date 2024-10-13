using GameLogic.Extensions;
using GameLogic.Helpers;
using SuperPlayer.Interfaces;
using System.Net.WebSockets;
using GameLogic.Messages.Requests;
using SuperPlayer.Helpers;
using GameLogic.Types;

namespace SuperPlayer.PlayerCommands
{
    public class LoginCommand : IPlayerCommand
    {
        private long _userDeviceId { get; init; }

        public LoginCommand(long clientId) 
        {
            _userDeviceId = clientId;
        }

        public async Task Execute(WebSocket webSocket)
        {
            string payload = ComputePayloadData();

            if (payload.IsNullOrEmpty())
            {
                Console.WriteLine("Payload data was not done correctly.");
                await Task.Delay(2000);
                return;
            }

            await TransferDataHelper.SendTextOverChannel(webSocket, payload);

            //await RecieveDataFromServerHelper.RecieveServerMessageOverChannel(webSocket);

            //_ = Task.Run(() => HandleServerNotificaions(webSocket));
        }

        private string ComputePayloadData()
        {
            return new LoginRequestMessage(_userDeviceId).ToString();
        }
    }
}
