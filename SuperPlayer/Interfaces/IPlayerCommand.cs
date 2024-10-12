using System.Net.WebSockets;

namespace SuperPlayer.Interfaces
{
    public interface IPlayerCommand
    {
        public void ComputePayloadData();
        public Task Execute(WebSocket webSocket);
    }
}
