namespace SuperServer.Database
{
    /// <summary>
    /// Repository class made to track the connections and the udids
    /// </summary>
    public static class ClientIdUdidRepository
    {
        private static Dictionary<long, long> ClientIdUdid { get; set; } = [];

        public static void AddConnectionMapping(long clientId, long playerUdid)
        {
            ClientIdUdid.TryAdd(clientId, playerUdid);
        }

        public static void RemoveConnectionMapping(long clientId) 
        {
            ClientIdUdid.Remove(clientId);
        }

        public static bool GetPlayerUdid(long clientId, out long value)
        {
            return ClientIdUdid.TryGetValue(clientId, out value);
        }
    }
}
