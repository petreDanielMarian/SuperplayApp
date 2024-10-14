using GameLogic.Model;
using Serilog;
using SuperPlayer.Interfaces;

namespace SuperPlayer.ResponseHandlers
{
    public class LoginResponseHandler() : IResponseHandler
    {
        public async Task HandleResponse(string response)
        {
            Log.Information($"Login response: {response}");

            if (response.Contains('-'))
            {
                response = response.TrimStart('-');
                Console.WriteLine($"You are already logged in! Player id: {response}");
                await Task.Delay(3000);
            }
            else
            {
                Console.WriteLine($"Registration done, your player id is {response}");
                await Task.Delay(3000);
            }

            Client.GetInstance.ActivePlayer = new Player(long.Parse(response));
        }
    }
}
