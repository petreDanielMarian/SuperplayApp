using GameLogic.Types;

namespace GameLogic.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if a string is null or empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if the string is null or empty value</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return value == null || value.Length == 0;
        }

        /// <summary>
        /// Converts string (character) to a <see cref="PlayerResourceType"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns>The according <see cref="PlayerResourceType"/></returns>
        public static PlayerResourceType ToPlayerResourceType(this string type) => type.ToLowerInvariant() switch
        {
            "c" => PlayerResourceType.Coins,
            "r" => PlayerResourceType.Rolls,
            _ => PlayerResourceType.None
        };


        /// <summary>
        /// Converts string (character) to a <see cref="PlayeCommandTyperResourceType"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns>The according <see cref="CommandType"/></returns>
        public static CommandType ToSupportedCommand(this string input) => input.ToLowerInvariant() switch
        {
            "l" => CommandType.Login,
            "o" => CommandType.Logout,
            "u" => CommandType.UpdateResources,
            "s" => CommandType.SendGift,
            "e" => CommandType.Exit,
            _ => CommandType.Retry
        };
    }
}
