namespace SuperServer.Database
{
    public static class ClientsRepository
    {
        public static List<long> ActiveClientConnection = new List<long>();

        public static void AddNewConnection(long id)
        {
            ActiveClientConnection.Add(id);
        }
        public static void RemoveConnection(long id)
        {
            ActiveClientConnection.Remove(id);
        }
    }
}
