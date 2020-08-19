﻿using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async;
using FunctionalXUnit.Models.Mocks.Implementation;
using Xunit;

namespace FunctionalXUnit.Models.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для асинхронного преобразования типов . Тесты
    /// </summary>
    public class MapBindAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка преобразования типов с помощью функции. Из числа в строку
        /// </summary>
        [Fact]
        public async Task MapAsync_IntToString()
        {
            const int number = 2;

            string stringFromNumber = await number.MapAsync(AsyncFunctions.IntToStringAsync);

            Assert.Equal(number.ToString(), stringFromNumber);
        }
    }
}