using System.Text;

namespace GameLogic.Helpers
{
    public static class ConsoleHelper
    {
        public static string? ExternalInput = null;
        public static bool HasExternalInput;

        /// <summary>
        /// This is generated with ChatGpt. I needed a way to mark the "notification" from server and update the view in the console client
        /// Prompt: I have this while that waits for a ReadLine() but another thread writes in the meantime "SomeText" in the console. How can I consider this a "Line" so the code proceedes?
        /// </summary>
        /// <returns>An user inserted string OR the string in ExternalInput, only if the HasExternalInput is set to True</returns>
        public static string ReadFromConsoleExternal()
        {
            StringBuilder inputBuilder = new StringBuilder();

            while (true)
            {
                // Check if there's external input
                if (HasExternalInput)
                {
                    string external = ExternalInput ??= string.Empty;
                    ExternalInput = null; // Clear the input
                    HasExternalInput = false;
                    return external; // Return external input as the line
                }

                // Check if there's user input
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                    // Handle Enter key (end of line)
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine(); // Move to the next line
                        return inputBuilder.ToString(); // Return the constructed input line
                    }

                    // Handle Backspace (remove the last character)
                    if (keyInfo.Key == ConsoleKey.Backspace && inputBuilder.Length > 0)
                    {
                        inputBuilder.Remove(inputBuilder.Length - 1, 1);
                        Console.Write("\b \b"); // Erase the character from the console
                    }
                    else if (keyInfo.Key != ConsoleKey.Backspace)
                    {
                        // Add character to input and display it
                        inputBuilder.Append(keyInfo.KeyChar);
                        Console.Write(keyInfo.KeyChar);
                    }
                }

                // Small delay to prevent high CPU usage
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// This will not return null
        /// </summary>
        /// <returns>The input or empty string in case the ReadLine() returns null</returns>
        public static string ReadLineSafelyFromConsole()
        {
            string? input = Console.ReadLine();

            return input ?? string.Empty;
        }
    }
}
