using GameLogic.Types;
using SuperPlayer.Interfaces;
using SuperPlayer.PlayerCommands;

namespace SuperPlayer.Factories
{
    /// <summary>
    /// Factory class to get the appropiate command runner
    /// </summary>
    /// <param name="clientId">The client id</param>
    public class PlayerCommandFactory(long clientId)
    {
        public IPlayerCommand GetCommand(CommandType commandType) => commandType switch
        {
            CommandType.Login => new LoginCommand(clientId),
            CommandType.UpdateResources => new UpdateResourcesCommand(clientId),
            CommandType.SendGift => new SendGiftCommand(clientId),
            CommandType.Exit => new ExitCommand(clientId),
            _ => throw new NotImplementedException("More features to come! Stay tuned!"),
        };
    }
}
