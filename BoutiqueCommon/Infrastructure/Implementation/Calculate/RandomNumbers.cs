using System;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueCommon.Infrastructure.Implementation.Calculate
{
    /// <summary>
    /// Случайные числа
    /// </summary>
    public static class RandomNumbers
    {
        /// <summary>
        /// Получить случайное число
        /// </summary>
        public static int GetRandom(int minimum, int maximum) =>
            new Random().
            Map(random => random.Next(minimum, maximum));
    }
}