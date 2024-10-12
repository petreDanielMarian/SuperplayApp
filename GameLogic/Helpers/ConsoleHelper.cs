namespace GameLogic.Helpers
{
    public static class ConsoleHelper
    {
        public static string ReadLineSafelyFromConsole()
        {
            string? input = Console.ReadLine();

            return input ?? string.Empty;
        }
    }
}
