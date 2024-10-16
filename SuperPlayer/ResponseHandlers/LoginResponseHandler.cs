using GameLogic.Extensions;
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

            if (response.Equals("0 0 0")) // EmptyPlayer
            {
                Console.WriteLine($"You cannot login!");
                await Task.Delay(1500);
            }
            else
            {
                if (response.Split()[0].Equals(Client.GetInstance.ActivePlayer.Id))
                {
                    Console.WriteLine("You are logged in already");
                    await Task.Delay(1500);
                }
                else
                {
                    Console.WriteLine($"Login done, your player id is {response.Split()[0]}");
                    await Task.Delay(1500);
                }

                Client.GetInstance.ActivePlayer = ToPlayerDataFromResponse(response);
            }
        }

        private static Player ToPlayerDataFromResponse(string resposne)
        {
            string[] tokens = resposne.Split();
            return new Player(long.Parse(tokens[0]), int.Parse(tokens[1]), int.Parse(tokens[2]));
        }
    }
}
