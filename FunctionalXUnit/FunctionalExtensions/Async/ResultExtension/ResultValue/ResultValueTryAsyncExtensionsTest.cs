using System;
using System.Linq;
using System.Threading.Tasks;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using FunctionalXUnit.Data;
using Xunit;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultValue.ResultValueTryAsyncExtensions;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class ResultValueTryAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultValueTry_Ok()
        {
            var resultValue = await ResultValueTryAsync(() => DivisionAsync(1), Exceptions.FuncExceptionToError);

            Assert.True(resultValue.OkStatus);
            Assert.Equal(await DivisionAsync(1), resultValue.Value);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueTry_Exception()
        {
            var resultValue = await ResultValueTryAsync(() => DivisionAsync(0), Exceptions.FuncExceptionToError);

            Assert.True(resultValue.HasErrors);
            Assert.Equal(ErrorResultType.DevideByZero, resultValue.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Функция деления на ноль
        /// </summary>
        private static async Task<int> DivisionAsync(int divider) => await Task.FromResult(10 / divider);
    }
}