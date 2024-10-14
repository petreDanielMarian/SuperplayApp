namespace GameLogic.Messages.Requests
{
    public class ConnectionRequest(string clientId, string encryptedClientId)
    {
        public override string ToString()
        {
            return $"{clientId} {encryptedClientId}";
        }
    }
}
