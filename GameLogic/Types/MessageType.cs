namespace GameLogic.Types
{
    /// <summary>
    /// Type of message:
    /// ServerNotification -> if a notification is sent
    /// ServerCommunication -> if a response is sent
    /// </summary>
    public enum MessageType
    {
        Unknown = 0,
        ServerCommunication = 1,
        ServerNotification = 2
    }
}
