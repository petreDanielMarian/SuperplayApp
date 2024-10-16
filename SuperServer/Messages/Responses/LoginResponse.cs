using GameLogic.Model;
using GameLogic.Types;

namespace SuperServer.Messages.Responses
{
    public class LoginResponse(Player loggedPlayer) : BaseResponse
    {
        public override string ToString()
        {
            return $"{(int)MessageType.ServerCommunication} {(int)CommandType.Login} {loggedPlayer.Id} {loggedPlayer.Resources[PlayerResourceType.Coins]} {loggedPlayer.Resources[PlayerResourceType.Rolls]}";
        }
    }
}
