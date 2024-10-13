using GameLogic.Types;
using System.Security.AccessControl;

namespace GameLogic.Messages.Responses
{
    public class SendGiftResponse(long playerId, PlayerResourceType resourceType, int amount) : BaseResponse
    {
        public override string ToString()
        {
            return $"{(int)MessageType.ServerCommunication} {(int)CommandType.SendGift} {playerId} {(int)resourceType} {amount}";
        }
    }
}
