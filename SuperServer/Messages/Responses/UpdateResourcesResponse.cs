using GameLogic.Types;

namespace SuperServer.Messages.Responses
{
    public class UpdateResourcesResponse(int resourceType, int amount) : BaseResponse
    {
        public override string ToString()
        {
            return $"{(int)MessageType.ServerCommunication} {(int)CommandType.UpdateResources} {resourceType} {amount}";
        }
    }
}
