using SuperPlayer;
using System.Net.WebSockets;

public class Program
{
    private static async Task Main()
    {
        var client = Client.GetInstance;

        try
        {
            await client.ConnectToServer();

            await client.HandleServerCommunication();
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            await client.WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            client.WebSocket.Dispose();
        }
    }
}