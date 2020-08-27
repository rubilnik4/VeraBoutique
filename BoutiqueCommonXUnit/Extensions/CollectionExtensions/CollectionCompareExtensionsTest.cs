using System.Collections.Generic;
using BoutiqueCommon.Extensions.CollectionExtensions;
using Xunit;

namespace BoutiqueCommonXUnit.Extensions.CollectionExtensions
{
    /// <summary>
    /// Сравнение двух коллекций. Тесты
    /// </summary>
    public class CollectionCompareExtensionsTest
    {
        /// <summary>
        /// Сравнить две коллекции по функции. Идентичны
        /// </summary>
        [Fact]
        public void CompareByFunc_Equal()
        {
            var numbers = new List<int> { 1, 2, 3 };
            var numbersString = new List<string> { "1", "2", "3" };

            bool isEqual = numbers.CompareByFunc(numbersString, (number, numberString) => number.ToString() == numberString);

            Assert.True(isEqual);
        }

        /// <summary>
        /// Сравнить две коллекции по функции. Не идентичны по количеству
        /// </summary>
        [Fact]
        public void CompareByFunc_NotEqualByCount()
        {
            var numbersFirst = new List<int> { 1, 2, 3 };
            var numbersSecond = new List<int> { 1, 2, 3, 4, 5 };

            bool isEqual = numbersFirst.CompareByFunc(numbersSecond, (numberFirst, numberSecond) => numberFirst == numberSecond);

            Assert.False(isEqual);
        }
    }
}