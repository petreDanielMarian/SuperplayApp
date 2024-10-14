namespace GameLogic.Messages.Responses
{
    public class ConnectionResponse(string response)
    {
        public override string ToString()
        {
            return $"{response}";
        }
    }
}
