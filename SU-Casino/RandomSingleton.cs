using System;

namespace SU_Casino
{
    internal class RandomSingleton
    {
        private static Random random = new Random();
        private static readonly Object padLockRandom = new Object();

        private RandomSingleton() { }

        public static int Next(int minValue, int lowerThenMaxValue)
        {
            lock (padLockRandom)
            {
                return random.Next(minValue, lowerThenMaxValue);
            }            
        }

        internal static double NextDouble()
        {
            lock (padLockRandom)
            {
                return random.NextDouble();
            }
        }
    }
}