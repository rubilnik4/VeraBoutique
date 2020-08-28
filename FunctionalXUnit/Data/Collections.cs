using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalXUnit.Data
{
    /// <summary>
    /// Тестовые примеры коллекций
    /// </summary>
    public static class Collections
    {
        /// <summary>
        /// Список чисел
        /// </summary>
        public static IReadOnlyCollection<int> GetRangeNumber() =>
            Enumerable.Range(0, 3).ToList().AsReadOnly();

        /// <summary>
        /// Преобразовать список чисел в строку
        /// </summary>
        public static string AggregateToString(IEnumerable<int> numbers) =>
            numbers.Aggregate(String.Empty, (previous, next) => previous.ToString() + next.ToString());
    }
}