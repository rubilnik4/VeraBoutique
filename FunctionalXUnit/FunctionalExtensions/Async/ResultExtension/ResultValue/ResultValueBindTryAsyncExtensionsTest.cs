using System;
using System.Linq;
using System.Threading.Tasks;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using FunctionalXUnit.Data;
using FunctionalXUnit.Mocks.Implementation;
using Xunit;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultValue.ResultValueBindTryAsyncExtensions;
using static FunctionalXUnit.Data.ErrorData;
using static FunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class ResultValueBindTryAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryAsync_Ok()
        {
            var resultValue = await ResultValueBindTryAsync(() => Task.FromResult((IResultValue<int>)new ResultValue<int>(Division(1))),
                                                            Exceptions.ExceptionError());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(await AsyncFunctions.DivisionAsync(1), resultValue.Value);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryAsync_Exception()
        {
            var resultValue = await ResultValueBindTryAsync(() => Task.FromResult((IResultValue<int>)new ResultValue<int>(Division(0))), Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.Equal(ErrorResultType.DevideByZero, resultValue.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkAsync_OkResult_OkTry()
        {
            const int initialNumber = 2;
            var numberResult = new ResultValue<int>(initialNumber);

            var numberAfterTry = await numberResult.ResultValueBindTryOkAsync(
                numbers => Task.FromResult((IResultValue<int>)new ResultValue<int>(Division(numbers))), CreateErrorTest());

            Assert.True(numberAfterTry.OkStatus);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialNumber), numberAfterTry.Value);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkAsync_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultValue<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueBindTryOkAsync(
                numbers => Task.FromResult((IResultValue<int>)new ResultValue<int>(Division(numbers))), CreateErrorTest());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkAsync_OkResult_ExceptionTry()
        {
            const int initialNumber = 0;
            var numberResult = new ResultValue<int>(initialNumber);

            var numberAfterTry = await numberResult.ResultValueBindTryOkAsync(
                numbers => Task.FromResult((IResultValue<int>)new ResultValue<int>(Division(numbers))), CreateErrorTest());

            Assert.True(numberAfterTry.HasErrors);
            Assert.Equal(ErrorResultType.DevideByZero, numberAfterTry.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkAsync_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultValue<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueBindTryOkAsync(
                numbers => Task.FromResult((IResultValue<int>)new ResultValue<int>(Division(numbers))), CreateErrorTest());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}