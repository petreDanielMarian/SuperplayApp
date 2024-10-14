namespace SuperServer.Messages.Responses
{
    public class ConnectionResponse(string response)
    {
        public override string ToString()
        {
            return $"{response}";
        }
    }
}
