using GameLogic.Types;

namespace GameLogic.Messages.Requests
{
    public class UpdateResourcesRequest(long playerId, int resourceType, int amount)
    {
        public override string ToString()
        {
            return $"{(int)CommandType.UpdateResources} {playerId} {resourceType} {amount}";
        }
    }
}
