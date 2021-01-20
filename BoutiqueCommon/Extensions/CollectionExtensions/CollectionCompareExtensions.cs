using System;
using System.Collections.Generic;
using System.Linq;

namespace BoutiqueCommon.Extensions.CollectionExtensions
{
    /// <summary>
    /// Сравнение двух коллекций
    /// </summary>
    public static class CollectionCompareExtensions
    {
        /// <summary>
        /// Сравнить две коллекции по функции
        /// </summary>
        public static bool CompareByFunc<TValueFirst, TValueSecond>(this IReadOnlyCollection<TValueFirst> collectionFirst,
                                                                    IReadOnlyCollection<TValueSecond> collectionSecond,
                                                                    Func<TValueFirst, TValueSecond, bool> compareFunc) =>
            collectionFirst.Count == collectionSecond.Count &&
            collectionFirst.Zip(collectionSecond, (first, second) => (first, second)).
                            All(tuple => compareFunc(tuple.first, tuple.second));
    }
}