using GameLogic.Model;
using Serilog;
using SuperPlayer.Interfaces;
using System.Net.WebSockets;

namespace SuperPlayer.ResponseHandlers
{
    class LogoutResponseHandler() : IResponseHandler
    {
        public async Task HandleResponse(string response)
        {
            Log.Information($"Logout response: {response}");

            if (response.Equals("OK"))
            {
                Console.WriteLine($"You logged out");
                Client.GetInstance.ActivePlayer = Player.EmptyPlayer;
            }

            await Task.Delay(1000);
        }
    }
}
