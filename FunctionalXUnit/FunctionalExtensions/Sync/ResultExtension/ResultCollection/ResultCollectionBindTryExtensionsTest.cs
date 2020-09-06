using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using System.Linq;
using Functional.Models.Enums;
using FunctionalXUnit.Data;
using FunctionalXUnit.Mocks.Implementation;
using Xunit;
using static FunctionalXUnit.Data.Collections;
using static FunctionalXUnit.Data.ErrorData;
using static FunctionalXUnit.Mocks.Implementation.SyncFunctions;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection.ResultCollectionBindTryExtensions;
namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultCollection
{
    /// <summary>
    /// Методы расширения для результирующего ответа с со связыванием коллекцией и обработкой исключений. Тесты
    /// </summary>
    public class ResultCollectionBindTryExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public void ResultCollectionBindTry_Ok()
        {
            var resultCollection = ResultCollectionBindTry(() => new ResultCollection<int>(DivisionCollection(1)),
                                                           Exceptions.ExceptionError());

            Assert.True(resultCollection.OkStatus);
            Assert.True(DivisionCollection(1).SequenceEqual(resultCollection.Value));
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void ResultCollectionBindTry_Exception()
        {
            var resultValue = ResultCollectionBindTry(() => new ResultCollection<int>(DivisionCollection(0)), Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.Equal(ErrorResultType.DevideByZero, resultValue.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOk_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = new ResultCollection<int>(initialNumbers);

            var numberAfterTry = numbersResult.ResultCollectionBindTryOk(numbers => new ResultCollection<int>(DivisionByCollection(numbers)), CreateErrorTest());

            Assert.True(numberAfterTry.OkStatus);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numberAfterTry.Value));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = new ResultCollection<int>(initialError);

            var numberAfterTry = numbersResult.ResultCollectionBindTryOk(numbers => new ResultCollection<int>(DivisionByCollection(numbers)), CreateErrorTest());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numberResult = new ResultCollection<int>(initialNumbers);

            var numberAfterTry = numberResult.ResultCollectionBindTryOk(numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.Equal(ErrorResultType.DevideByZero, numberAfterTry.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultCollection<int>(initialError);

            var numberAfterTry = numberResult.ResultCollectionBindTryOk(numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}