namespace GameLogic.Helpers
{
    public static class RandomNumbersPool
    {
        private static List<long> _generatedNumbers = new List<long>();

        /// <summary>
        /// Gets a number that was not used in this session
        /// </summary>
        /// <returns>An unique long number between [1-9] </returns>
        public static long GetUniqueServerId()
        {
            return GetUniqueId(0, 10);
        }

        /// <summary>
        /// Gets a number that was not used in this session
        /// </summary>
        /// <returns>An unique long number between [10-99] </returns>
        public static long GetUniqueClientId()
        {
            return GetUniqueId(10, 100);
        }

        /// <summary>
        /// Gets a number that was not used in this session
        /// </summary>
        /// <returns>An unique long number between [100-999] </returns>
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
