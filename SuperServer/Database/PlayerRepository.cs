using GameLogic.Model;
using GameLogic.Types;

namespace SuperServer.Database
{
    /// <summary>
    /// Player table structure for quick run (In-Memory state save)
    /// For more server instances, a database is a more suitable choice
    /// </summary>
    public static class PlayerRepository
    {
        private static Dictionary<long, Player> RegisteredPlayers = [];
        private static Dictionary<long, PlayerConnection> ActivePlayers = [];

        /// <summary>
        /// Adds player to registered players and also marks it as active
        /// </summary>
        /// <param name="uniqueDeviceId"></param>
        /// <param name="playerConnection"></param>
        public static void RegisterPlayer(long uniqueDeviceId, PlayerConnection playerConnection)
        {
            RegisteredPlayers.TryAdd(uniqueDeviceId, playerConnection.Player);
            ActivePlayers.Add(uniqueDeviceId, playerConnection);
        }

        /// <summary>
        /// Get all registered players connected to this server
        /// </summary>
        /// <returns></returns>
        public static Dictionary<long, Player> GetAllRegisteredPlayers() => RegisteredPlayers;

        /// <summary>
        /// Get all active players connected to this server
        /// </summary>
        /// <returns></returns>
        public static Dictionary<long, PlayerConnection> GetAllActivePlayers() => ActivePlayers;

        /// <summary>
        /// Remove active player (mark player as inactive -> logged out)
        /// </summary>
        /// <param name="uniqueDeviceId"></param>
        public static void RemoveActivePlayer(long uniqueDeviceId)
        {
            ActivePlayers.Remove(uniqueDeviceId);
        }

        /// <summary>
        /// Get active player connection based on its player id
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns>Returns <see cref="PlayerConnection"/> with <see cref="Player"/> and WebSocket or null if it does not exist</returns>
        public static PlayerConnection? GetActivePlayerByPlayerId(long playerId)
        {
            return ActivePlayers.Values.FirstOrDefault(pc => pc.Player.Id == playerId);
        }

        /// <summary>
        /// Get registered player based on its player id
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns>Returns  <see cref="Player"/> or null</returns>
        public static Player? GetRegisteredPlayerByPlayerId(long playerId)
        {
            return RegisteredPlayers.Values.FirstOrDefault(player => player.Id == playerId);
        }

        /// <summary>
        /// Updates player's resource with specified amount
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="resourceTypetype"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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
        /// Updates player's resource, removing the specified amount
        /// </summary>
        /// <param name="playerId">Player ID to get the player</param>
        /// <param name="resourceTypetype">Resource type (coins or rolls)</param>
        /// <param name="amount">Number of resrouces to be removed</param>
        /// <returns>True if everyting goes well, false if the amount is greater than the actual reserve of resources</returns>
        /// <exception cref="ArgumentNullException"></exception>
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
