using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Extensions.CollectionExtensions;
using BoutiqueCommonXUnit.Models.StringCollection;
using Xunit;

namespace BoutiqueCommonXUnit.Extensions.CollectionExtensions
{
    /// <summary>
    /// Методы расширения множеств. Тесты
    /// </summary>
    public class EnumerableExtensionsTest
    {
        /// <summary>
        /// Объединение двух коллекций. Равенство
        /// </summary>
        [Fact]
        public void ZipLong_Equal()
        {
            var collection = new List<string> { "first", "second", "third" };

            var collectionZip = collection.ZipLong(collection, (first, second) => new StringTuple(first, second)).ToList();

            Assert.True(collectionZip.Select(stringTuple => stringTuple.FirstString).
                                      SequenceEqual(collection));
            Assert.True(collectionZip.Select(stringTuple => stringTuple.SecondString).
                                      SequenceEqual(collection));
        }

        /// <summary>
        /// Объединение двух коллекций. Равенство
        /// </summary>
        [Fact]
        public void ZipLong_SecondNull()
        {
            var collectionFirst = new List<string> { "first", "second", "third" };
            var collectionSecond = collectionFirst.Take(2).ToList();

            var collectionZip = collectionFirst.ZipLong(collectionSecond, (first, second) => new StringTuple(first, second)).ToList();

            Assert.True(collectionZip.Select(stringTuple => stringTuple.FirstString).
                                      SequenceEqual(collectionFirst));
            Assert.True(collectionZip.Select(stringTuple => stringTuple.SecondString).
                                      SequenceEqual(new List<string?>(collectionSecond).Append(null)));
        }

        /// <summary>
        /// Объединение двух коллекций. Равенство
        /// </summary>
        [Fact]
        public void ZipLong_FirstNull()
        {
            var collectionSecond = new List<string> { "first", "second", "third" };
            var collectionFirst = collectionSecond.Take(2).ToList();

            var collectionZip = collectionFirst.ZipLong(collectionSecond, (first, second) => new StringTuple(first, second)).ToList();

            Assert.True(collectionZip.Select(stringTuple => stringTuple.FirstString).
                                      SequenceEqual(new List<string?>(collectionFirst).Append(null)));
            Assert.True(collectionZip.Select(stringTuple => stringTuple.SecondString).
                                      SequenceEqual(collectionSecond));
        }
    }
}