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

        /// <summary>
        /// Разбить массив на пачки
        /// </summary>
        [Fact]
        public void SelectChunk_Ok()
        {
            var collection = Enumerable.Range(1, 7);
            const int chunks = 3;

            var collectionChunked = collection.SelectChunk(chunks).ToList();

            Assert.Equal(3, collectionChunked.Count);
            Assert.Equal(3, collectionChunked[0].Count());
            Assert.Equal(3, collectionChunked[1].Count());
            Assert.Single(collectionChunked[2]);
        }

        /// <summary>
        /// Разбить массив на пачки. Превышение длины массива
        /// </summary>
        [Fact]
        public void SelectChunk_FirstOnly()
        {
            var collection = Enumerable.Range(1, 7).ToList();
            const int chunks = 8;

            var collectionChunked = collection.SelectChunk(chunks).ToList();

            Assert.Single(collectionChunked);
            Assert.Equal(collection.Count, collectionChunked[0].Count());
        }

        /// <summary>
        /// Разбить массив на пачки. Неверное значение деления
        /// </summary>
        [Fact]
        public void SelectChunk_OutOfRange()
        {
            var collection = Enumerable.Range(1, 7).ToList();
            const int chunks = 0;

            var collectionChunked = collection.SelectChunk(chunks).ToList();

            Assert.Single(collectionChunked);
            Assert.Equal(collection.Count, collectionChunked[0].Count());
        }
    }
}