using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        /// Преобразовать коллекцию чисел в коллекцию строк
        /// </summary>
        public static IEnumerable<string> CollectionToString(IEnumerable<int> numbers) =>
            numbers.Select(number => number.ToString());

        /// <summary>
        /// Преобразовать коллекцию чисел в коллекцию строк асинхронно
        /// </summary>
        public static async Task<IEnumerable<string>> CollectionToStringAsync(IEnumerable<int> numbers) =>
            await Task.FromResult(numbers.Select(number => number.ToString()));

        /// <summary>
        /// Преобразовать список чисел в строку
        /// </summary>
        public static string AggregateToString(IEnumerable<int> numbers) =>
            numbers.Aggregate(String.Empty, (previous, next) => previous.ToString() + next.ToString());
    }
}