using System;
using System.Linq;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using FunctionalXUnit.Data;
using FunctionalXUnit.Mocks.Implementation;
using Xunit;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue.ResultValueTryExtensions;
using static FunctionalXUnit.Data.ErrorData;
using static FunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValue
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений. Тесты
    /// </summary>
    public class ResultValueTryExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public void ResultValueTry_Ok()
        {
            var resultValue = ResultValueTry(() => SyncFunctions.Division(1), Exceptions.ExceptionError());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(SyncFunctions.Division(1), resultValue.Value);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueTry_Exception()
        {
            var resultValue = ResultValueTry(() => SyncFunctions.Division(0), Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.Equal(ErrorResultType.DevideByZero, resultValue.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultValueTryOk_OkResult_OkTry()
        {
            const int initialNumber = 2;
            var numberResult = new ResultValue<int>(initialNumber);

            var numberAfterTry = numberResult.ResultValueTryOk(Division, CreateErrorTest());

            Assert.True(numberAfterTry.OkStatus);
            Assert.Equal(Division(initialNumber), numberAfterTry.Value);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultValueTryOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultValue<int>(initialError);

            var numberAfterTry = numberResult.ResultValueTryOk(Division, CreateErrorTest());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public void ResultValueTryOk_OkResult_ExceptionTry()
        {
            const int initialNumber = 0;
            var numberResult = new ResultValue<int>(initialNumber);

            var numberAfterTry = numberResult.ResultValueTryOk(Division, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.Equal(ErrorResultType.DevideByZero, numberAfterTry.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public void ResultValueTryOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultValue<int>(initialError);

            var numberAfterTry = numberResult.ResultValueTryOk(Division, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}