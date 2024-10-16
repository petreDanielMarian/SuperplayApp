using GameLogic.Types;

namespace SuperPlayer.Messages.Requests
{
    internal class LogoutRequest()
    {
        public override string ToString()
        {
            return $"{(int)CommandType.Logout}";
        }
    }
}
