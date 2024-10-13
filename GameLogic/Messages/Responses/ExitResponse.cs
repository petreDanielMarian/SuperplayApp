using GameLogic.Types;

namespace GameLogic.Messages.Responses
{
    public class ExitResponse(string msg) : BaseResponse
    {
        public override string ToString()
        {
            return $"{(int)MessageType.ServerCommunication} {(int)CommandType.Exit} {msg}";
        }
    }
}
