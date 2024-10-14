using GameLogic.Types;

namespace SuperServer.Messages.Responses
{
    public class LoginResponse(long playerId) : BaseResponse
    {
        public override string ToString()
        {
            return $"{(int)MessageType.ServerCommunication} {(int)CommandType.Login} {playerId}";
        }
    }
}
