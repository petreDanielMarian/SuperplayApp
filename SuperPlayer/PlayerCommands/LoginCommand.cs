using GameLogic.Extensions;
using GameLogic.Helpers;
using GameLogic.Model;
using GameLogic.Types;
using SuperPlayer.Interfaces;
using System.Net.WebSockets;

namespace SuperPlayer.PlayerCommands
{
    public class LoginCommand : IPlayerCommand
    {
        private long _userDeviceId { get; init; }
        protected string Payload { get; private set; } = string.Empty;

        public LoginCommand(long clientId) 
        {
            _userDeviceId = clientId;
        }

        public async Task Execute(WebSocket webSocket)
        {
            if (Payload.IsNullOrEmpty())
            {
                Console.WriteLine("Payload data was not done correctly.");
                return;
            }

            await TransferDataHelper.SendTextOverChannel(webSocket, Payload);

            string playerId = await TransferDataHelper.RecieveTextOverChannel(webSocket);

            if (playerId.Contains('-'))
            {
                playerId = playerId.TrimStart('-');
                Console.WriteLine($"You are already logged in! Player id: {playerId}");
            }
            else
            {
                Console.WriteLine($"Registration done, your player id is {playerId}");
            }

            Client.GetInstance.ActivePlayer = new Player(long.Parse(playerId));
        }

        public void ComputePayloadData()
        {
            Payload = $"{(int)CommandType.Login} {_userDeviceId}";
        }
    }
}
