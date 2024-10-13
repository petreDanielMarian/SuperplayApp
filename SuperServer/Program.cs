using SuperServer;

Console.WriteLine("Hello, World!");

await StartServer();

static async Task StartServer()
{
    Server server = new Server();

    try
    {
        server.Start();

        await server.AwaitConnectionsAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
    finally
    {
        server.Shutdown();
    }
}
