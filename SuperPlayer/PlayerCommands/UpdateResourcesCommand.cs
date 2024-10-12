using SuperPlayer.Interfaces;
using System.Net.WebSockets;

namespace SuperPlayer.PlayerCommands
{
    internal class UpdateResourcesCommand : IPlayerCommand
    {
        private long _clientId;
        private string _payload = string.Empty;

        public UpdateResourcesCommand(long clientId)
        {
            _clientId = clientId;
        }

        public Task Execute(WebSocket webSocket)
        {
            throw new NotImplementedException();
        }

        public void ComputePayloadData()
        {
            throw new NotImplementedException();
        }
    }
}
