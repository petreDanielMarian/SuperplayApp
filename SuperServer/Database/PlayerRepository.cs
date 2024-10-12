using GameLogic.Model;

namespace SuperServer.Database
{
    public static class PlayerRepository
    {
        public static Dictionary<long, Player> RegisteredPlayers = [];
        public static Dictionary<long, Player> ActivePlayers = [];

        public static void RegisterPlayer(long userDeviceId, Player player)
        {
            RegisteredPlayers.TryAdd(userDeviceId, player);
            ActivePlayers.Add(userDeviceId, player);
        }

        public static void UnregisterPlayer(long userDeviceId)
        {
            RegisteredPlayers.Remove(userDeviceId);
        }
    }
}
