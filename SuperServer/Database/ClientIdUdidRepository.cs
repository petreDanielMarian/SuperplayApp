using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperServer.Database
{
    public static class ClientIdUdidRepository
    {
        private static Dictionary<long, long> ClientIdUdid { get; set; } = [];

        public static void AddConnectionMapping(long clientId, long playerUdid)
        {
            ClientIdUdid.Add(clientId, playerUdid);
        }

        public static void RemoveConnectionMapping(long clientId) 
        {
            ClientIdUdid.Remove(clientId);
        }

        public static long GetPlayerUdid(long clientId)
        {
            return ClientIdUdid[clientId];
        }
    }
}
