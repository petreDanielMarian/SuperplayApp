using GameLogic.Types;

namespace GameLogic.Messages.Requests
{
    public class ExitRequest
    {
        public override string ToString()
        {
            return $"{(int)CommandType.Exit}";
        }
    }
}
