using GameLogic.Model;
using GameLogic.Types;

namespace SuperServer.Database
{
    public static class PlayerRepository
    {
        public static Dictionary<long, Player> RegisteredPlayers = [];
        public static Dictionary<long, PlayerConnection> ActivePlayers = [];

        public static void RegisterPlayer(long uniqueDeviceId, PlayerConnection playerConnection)
        {
            RegisteredPlayers.TryAdd(uniqueDeviceId, playerConnection.Player);
            ActivePlayers.Add(uniqueDeviceId, playerConnection);
        }

        public static void UnregisterPlayer(long uniqueDeviceId)
        {
            RegisteredPlayers.Remove(uniqueDeviceId);
        }

        public static void RemoveActivePlayer(long uniqueDeviceId)
        {
            ActivePlayers.Remove(uniqueDeviceId);
        }

        public static PlayerConnection? GetActivePlayer(long uniqueDeviceId)
        {
            ActivePlayers.TryGetValue(uniqueDeviceId, out PlayerConnection? playerConnection);

            return playerConnection;
        }

        public static PlayerConnection? GetActivePlayerByPlayerId(long playerId)
        {
            return ActivePlayers.Values.FirstOrDefault(pc => pc.Player.Id == playerId);
        }

        public static Player? GetRegisteredPlayer(long uniqueDeviceId)
        {

            RegisteredPlayers.TryGetValue(uniqueDeviceId, out Player? player);

            return player;
        }

        public static Player? GetRegisteredPlayerByPlayerId(long playerId)
        {
            return RegisteredPlayers.Values.FirstOrDefault(player => player.Id == playerId);
        }

        public static Player AddPlayerResources(long playerId, PlayerResourceType resourceTypetype, int amount)
        {
            lock (RegisteredPlayers)
            {
                Player? targetedPlayer = GetRegisteredPlayerByPlayerId(playerId) ?? throw new ArgumentNullException($"No player found with the provided player ID {playerId}");

                targetedPlayer.Resources[resourceTypetype] += amount;
                return targetedPlayer;
            }
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="playerId">Player ID to get the player</param>
        /// <param name="resourceTypetype">Resource type (coins or rolls)</param>
        /// <param name="amount">Number of resrouces to be removed</param>
        /// <returns>True if everyting goes well, false if the amount is greater than the actual reserve of resources</returns>
        /// <throws>Exception if the user cannot be found as registered user</throws>
        public static Player RemovePlayerResources(long playerId, PlayerResourceType resourceTypetype, int amount)
        {
            lock (RegisteredPlayers)
            {
                Player? targetedPlayer = GetRegisteredPlayerByPlayerId(playerId) ?? throw new ArgumentNullException($"No player found with the provided player ID {playerId}");

                targetedPlayer.Resources[resourceTypetype] -= amount;

                return targetedPlayer;
            }
        }        
    }
}
