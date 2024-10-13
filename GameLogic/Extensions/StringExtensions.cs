using GameLogic.Types;

namespace GameLogic.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return value == null || value.Length == 0;
        }

        public static PlayerResourceType ToPlayerResourceType(this string type) => type.ToLowerInvariant() switch
        {
            "c" => PlayerResourceType.Coins,
            "r" => PlayerResourceType.Rolls,
            _ => PlayerResourceType.None
        };
    }
}
