using GameLogic.Types;
using SuperPlayer.Interfaces;
using SuperPlayer.PlayerCommands;

namespace SuperPlayer.Factories
{
    internal class PlayerCommandFactory
    {
        private long _clientId;

        public PlayerCommandFactory(long clientId)
        {
            _clientId = clientId;
        }

        public IPlayerCommand GetCommand(CommandType commandType) => commandType switch
        {
            CommandType.Login => new LoginCommand(_clientId),
            CommandType.UpdateResources => new UpdateResourcesCommand(_clientId),
            CommandType.SendGift => new SendGiftCommand(_clientId),
            CommandType.Exit => new ExitCommand(_clientId),
            _ => throw new NotImplementedException("More features to come! Stay tuned!"),
        };
    }
}
