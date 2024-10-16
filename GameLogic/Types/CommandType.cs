namespace GameLogic.Types
{
    /// <summary>
    /// Command type accepted by the server
    /// </summary>
    public enum CommandType
    {
        Retry = 0,
        Login = 1,
        UpdateResources = 2,
        SendGift = 3,
        Exit = 4,
        Logout = 5
    }
}
