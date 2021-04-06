using System;
using System.Collections.Generic;
using System.Linq;

namespace BoutiqueCommon.Extensions.CollectionExtensions
{
    /// <summary>
    /// Методы расширения множеств
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Объединение двух коллекцией
        /// </summary>
        public static IEnumerable<T> ZipLong<T1, T2, T>(this IEnumerable<T1> first, IEnumerable<T2> second,
                                                        Func<T1?, T2?, T> operation)
            where T1 : class
            where T2 : class
            where T : class
        {
            using var firstEnumerator = first.GetEnumerator();
            using var secondEnumerator = second.GetEnumerator();
            while (firstEnumerator.MoveNext())
            {
                if (secondEnumerator.MoveNext())
                {
                    yield return operation(firstEnumerator.Current, secondEnumerator.Current);
                }
                else
                {
                    yield return operation(firstEnumerator.Current, default);
                }
            }
            while (secondEnumerator.MoveNext())
            {
                yield return operation(default, secondEnumerator.Current);
            }
        }

        /// <summary>
        /// Разбить массив на пачки
        /// </summary>
        public static IEnumerable<IEnumerable<T>> SelectChunk<T>(this IEnumerable<T> collection, int chunks) =>
            chunks > 0 
                ? collection.
                  Select((value, index) => new { Index = index, Value = value }).
                  GroupBy(tuple => tuple.Index / chunks).
                  Select(group => group.Select(tuple => tuple.Value))
                : new List<IEnumerable<T>> { collection };
    }
}