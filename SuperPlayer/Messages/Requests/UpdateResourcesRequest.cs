using GameLogic.Types;

namespace SuperPlayer.Messages.Requests
{
    public class UpdateResourcesRequest(long clientId, long playerId, int resourceType, int amount)
    {
        public override string ToString()
        {
            return $"{(int)CommandType.UpdateResources} {clientId} {playerId} {resourceType} {amount}";
        }
    }
}
