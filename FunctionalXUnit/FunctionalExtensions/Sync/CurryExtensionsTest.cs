using System;
using Functional.FunctionalExtensions.Sync;
using Xunit;

namespace FunctionalXUnit.FunctionalExtensions.Sync
{
    /// <summary>
    /// Методы расширения для функций высшего порядка. Тесты
    /// </summary>
    public class CurryExtensionsTest
    {
        /// <summary>
        /// Проверка преобразования функции высшего порядка для одного аргумента
        /// </summary>
        [Fact]
        public void Curry_ReturnNoArgumentFunc()
        {
            Func<int, int> plusTwoFunc = number => number + 2;

            var totalFunc = plusTwoFunc.Curry(3);

            Assert.Equal(5, totalFunc.Invoke());
        }

        /// <summary>
        /// Проверка преобразования функции высшего порядка для двух аргументов
        /// </summary>
        [Fact]
        public void Curry_ReturnOneArgumentFunc()
        {
            Func<int, int, int> plusTwoFunc = (numberFirst, numberSecond) => numberFirst + numberSecond;

            var totalFunc = plusTwoFunc.Curry(3);

            Assert.Equal(5, totalFunc.Invoke(2));
        }

        /// <summary>
        /// Проверка преобразования функции высшего порядка для трех аргументов
        /// </summary>
        [Fact]
        public void Curry_ReturnTwoArgumentFunc()
        {
            Func<int, int, int, int> plusTwoFunc = 
                (numberFirst, numberSecond, thirdNumber) => numberFirst + numberSecond + thirdNumber;

            var totalFunc = plusTwoFunc.Curry(3);

            Assert.Equal(6, totalFunc.Invoke(2, 1));
        }
    }
}