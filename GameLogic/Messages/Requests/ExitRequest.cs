using GameLogic.Types;

namespace GameLogic.Messages.Requests
{
    public class ExitRequest(long clientId)
    {
        public override string ToString()
        {
            return $"{(int)CommandType.Exit} {clientId} {clientId} ";
        }
    }
}
