using GameLogic.Types;

namespace SuperServer.Messages.Responses
{
    public class SendGiftResponse(long playerId, int resourceType, int amount) : BaseResponse
    {
        public override string ToString()
        {
            return $"{(int)MessageType.ServerCommunication} {(int)CommandType.SendGift} {playerId} {resourceType} {amount}";
        }
    }
}
