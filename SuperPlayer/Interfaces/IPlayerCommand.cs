using System.Net.WebSockets;

namespace SuperPlayer.Interfaces
{
    /// <summary>
    /// Base interface for the command runners to implement
    /// </summary>
    public interface IPlayerCommand
    {
        /// <summary>
        /// Execute the PlayerCommand 
        /// </summary>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        public Task Execute(WebSocket webSocket);
    }
}
