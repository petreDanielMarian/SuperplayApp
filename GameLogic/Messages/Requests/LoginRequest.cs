using GameLogic.Types;

namespace GameLogic.Messages.Requests
{
    public class LoginRequest(long uuid)
    {
        public override string ToString()
        {
            return $"{(int)CommandType.Login} {uuid} {uuid}";
        }
    }
}
