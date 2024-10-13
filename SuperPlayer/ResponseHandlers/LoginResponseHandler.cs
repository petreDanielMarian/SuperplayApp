using GameLogic.Model;
using SuperPlayer.Interfaces;
using System.Net.WebSockets;

namespace SuperPlayer.ResponseHandlers
{
    public class LoginResponseHandler() : IResponseHandler
    {
        public async Task HandleResponse(string response)
        {
            if (response.Contains('-'))
            {
                response = response.TrimStart('-');
                Console.WriteLine($"\nYou are already logged in! Player id: {response}");
                await Task.Delay(3000);
            }
            else
            {
                Console.WriteLine($"\nRegistration done, your player id is {response}");
                await Task.Delay(3000);
            }

            Client.GetInstance.ActivePlayer = new Player(long.Parse(response));
        }
    }
}
