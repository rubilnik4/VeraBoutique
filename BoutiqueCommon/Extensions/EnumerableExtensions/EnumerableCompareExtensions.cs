using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoutiqueCommon.Extensions.EnumerableExtensions
{
    /// <summary>
    /// Сравнение двух множеств
    /// </summary>
    public static class EnumerableCompareExtensions
    {
        /// <summary>
        /// Сравнить две коллекции по функции
        /// </summary>
        public static bool CompareByFunc<TValue>(this IReadOnlyCollection<TValue> collectionFirst,
                                                 IReadOnlyCollection<TValue> collectionSecond,
                                                 Func<TValue, TValue, bool> compareFunc) =>
            collectionFirst.Count == collectionSecond.Count &&
            collectionFirst.Zip(collectionSecond).All(tuple => compareFunc(tuple.First, tuple.Second));
    }
}