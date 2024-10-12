namespace GameLogic.Helpers
{
    public static class RandomNumbersPool
    {
        private static List<long> _generatedNumbers = new List<long>();

        public static long GetUniqueServerId()
        {
            return GetUniqueId(0, 10);
        }

        public static long GetUniqueClientId()
        {
            return GetUniqueId(10, 100);
        }

        public static long GetUniquePlayerId()
        {
            return GetUniqueId(100, 1000);
        }

        private static long GetUniqueId(long minLimit, long maxLimit)
        {
            long generatedNumber;
            while (_generatedNumbers.Contains(generatedNumber = new Random().NextInt64(minLimit, maxLimit)))
            {
                continue;
            }
            _generatedNumbers.Add(generatedNumber);

            return generatedNumber;
        }
    }
}
