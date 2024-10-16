using GameLogic.Types;
using SuperPlayer.Interfaces;
using SuperPlayer.PlayerCommands;

namespace SuperPlayer.Factories
{
    /// <summary>
    /// Factory class to get the appropiate command runner
    /// </summary>
    /// <param name="clientId">The client id</param>
    public class PlayerCommandFactory()
    {
        public IPlayerCommand GetCommand(CommandType commandType) => commandType switch
        {
            CommandType.Login => new LoginCommand(),
            CommandType.UpdateResources => new UpdateResourcesCommand(),
            CommandType.SendGift => new SendGiftCommand(),
            CommandType.Exit => new ExitCommand(),
            CommandType.Logout => new LogoutCommand(),
            _ => throw new NotImplementedException("More features to come! Stay tuned!"),
        };
    }
}
