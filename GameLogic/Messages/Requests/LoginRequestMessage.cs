using GameLogic.Messages.Responses;
using GameLogic.Types;

namespace GameLogic.Messages.Requests
{
    public class LoginRequestMessage(long uuid) : BaseResponse
    {
        public long UniqueDeviceId => uuid;

        public override string ToString()
        {
            return $"{(int)CommandType.Login} {uuid}";
        }
    }
}
