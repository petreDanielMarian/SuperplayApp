using GameLogic.Types;

namespace SuperServer.Messages.Responses
{
    internal class LogoutResponse(string payload) : BaseResponse
    {
        public override string ToString()
        {
            return $"{(int)MessageType.ServerCommunication} {(int)CommandType.Logout} {payload}";
        }
    }
}
