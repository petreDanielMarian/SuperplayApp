using Serilog;
using SuperServer;

await StartServer();

static async Task StartServer()
{
    Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

    Server server = new Server();

    try
    {
        server.Start();

        await server.AwaitConnectionsAsync();
    }
    catch (Exception ex)
    {
        Log.Error(ex, ex.ToString());
    }
    finally
    {
        server.Shutdown();
    }
}
