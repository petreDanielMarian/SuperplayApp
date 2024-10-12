using SuperPlayer.Interfaces;
using System.Net.WebSockets;

namespace SuperPlayer.PlayerCommands
{
    internal class SendGiftCommand : IPlayerCommand
    {

        private long _clientId;
        private string _payload = string.Empty;

        public SendGiftCommand(long clientId)
        {
            _clientId = clientId;
        }

        public void ComputePayloadData()
        {
            throw new NotImplementedException();
        }

        public Task Execute(WebSocket webSocket)
        {
            throw new NotImplementedException();
        }
    }
}
