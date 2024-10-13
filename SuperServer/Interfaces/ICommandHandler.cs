namespace SuperServer.Interfaces
{
    /// <summary>
    /// Base interface for the command handlers to implement
    /// </summary>
    public interface ICommandHandler
    {
        Task Handle();
    }
}
