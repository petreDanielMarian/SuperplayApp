using GameLogic.Types;

namespace GameLogic.Messages.Responses
{
    public class UpdateResourcesResponse(PlayerResourceType resourceType, int amount) : BaseResponse
    {
        public override string ToString()
        {
            return $"{(int)MessageType.ServerCommunication} {(int)CommandType.UpdateResources} {(int)resourceType} {amount}";
        }
    }
}
