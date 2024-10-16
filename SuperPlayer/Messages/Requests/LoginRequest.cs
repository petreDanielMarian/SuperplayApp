using GameLogic.Types;

namespace SuperPlayer.Messages.Requests
{
    public class LoginRequest(int udid)
    {
        public override string ToString()
        {
            return $"{(int)CommandType.Login} {udid}";
        }
    }
}
