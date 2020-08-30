using System.Collections.Generic;
using System.Linq;

namespace FunctionalXUnit.Mocks.Implementation
{
    /// <summary>
    /// Тестовые синхронные функции
    /// </summary>
    public static class SyncFunctions
    {
        /// <summary>
        /// Функция деления на ноль
        /// </summary>
        public static int Division(int divider) => 10 / divider;

        /// <summary>
        /// Функция деления на ноль коллекции
        /// </summary>
        public static IReadOnlyCollection<int> DivisionCollection(int divider) =>
            new List<int> { 10, 20, 30 }.
            Select(number => number / divider).
            ToList().AsReadOnly();
    }
}