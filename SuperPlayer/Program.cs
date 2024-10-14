using Polly.Retry;
using Serilog;
using SuperPlayer;
using System.Net.WebSockets;

public class Program
{
    private static async Task Main()
    {
        // I chose to log only to file on client side so the ui is still working through console
        Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        
        var client = Client.GetInstance;

        try
        {
            await client.ConnectToServerAsync();
            await client.HandleServerCommunicationAsync();
        }
        catch (Exception ex) 
        {
            Log.Error(ex.Message);
        }
        finally
        {
            //await client.WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            Console.WriteLine("No server was found.");
            client.WebSocket.Dispose();
        }
    }
}