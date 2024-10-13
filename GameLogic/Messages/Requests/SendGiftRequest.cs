using GameLogic.Types;

namespace GameLogic.Messages.Requests
{
    public class SendGiftRequest(long senderPlayerId, long recieverPlayerId, int resourceType, int amount)
    {
        public override string ToString()
        {
            return $"{(int)CommandType.SendGift} {senderPlayerId} {recieverPlayerId} {resourceType} {amount}";
        }
    }
}
