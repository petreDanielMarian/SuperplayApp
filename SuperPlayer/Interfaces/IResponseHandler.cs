namespace SuperPlayer.Interfaces
{
    /// <summary>
    /// Base interface for the response handlers to implement
    /// </summary>
    public interface IResponseHandler
    {
        Task HandleResponse(string response);
    }
}
