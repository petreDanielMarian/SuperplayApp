using System.Net.WebSockets;

namespace GameLogic.Model
{
    /// <summary>
    /// Holds the Player and the socket through which the server can communicate with
    /// </summary>
    /// <param name="player"></param>
    /// <param name="webSocket"></param>
    public class PlayerConnection(Player player, WebSocket webSocket)
    {
        public Player Player { get; init; } = player;
        public WebSocket WebSocket { get; init; } = webSocket;
    }
}
