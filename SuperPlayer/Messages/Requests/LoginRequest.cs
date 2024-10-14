using GameLogic.Types;

namespace SuperPlayer.Messages.Requests
{
    public class LoginRequest(long uuid)
    {
        public override string ToString()
        {
            return $"{(int)CommandType.Login} {uuid} {uuid}";
        }
    }
}
