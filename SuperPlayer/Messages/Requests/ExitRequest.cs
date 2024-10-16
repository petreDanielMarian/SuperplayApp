using GameLogic.Types;

namespace SuperPlayer.Messages.Requests
{
    public class ExitRequest(long playerId)
    {
        public override string ToString()
        {
            return $"{(int)CommandType.Exit} {playerId} ";
        }
    }
}
