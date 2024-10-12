using System.Net.WebSockets;

namespace SuperPlayer.Interfaces
{
    public interface IPlayerCommand
    {
        public Task Execute(WebSocket webSocket);
        public void ComputePayloadData();
    }
}
