using System.Net.WebSockets;

namespace GameLogic.Model
{
    public class PlayerConnection
    {
        public Player Player { get; init; }
        public WebSocket WebSocket { get; init; }

        public PlayerConnection(Player player, WebSocket webSocket)
        {
            WebSocket = webSocket;
            Player = player;
        }
    }
}
