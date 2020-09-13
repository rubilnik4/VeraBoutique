using System.Linq;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using FunctionalXUnit.Data;
using Xunit;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection.ResultCollectionTryExtensions;
using static FunctionalXUnit.Data.Collections;
using static FunctionalXUnit.Data.ErrorData;
using static FunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией и обработкой исключений. Тесты
    /// </summary>
    public class ResultCollectionTryExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с коллекцией
        /// </summary>
        [Fact]
        public void ResultCollectionTry_Ok()
        {
            int initialValue = Numbers.Number;

            var resultCollection = ResultCollectionTry(() => DivisionCollection(initialValue), Exceptions.ExceptionError());

            Assert.True(resultCollection.OkStatus);
            Assert.True(DivisionCollection(1).SequenceEqual( resultCollection.Value));
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с коллекцией
        /// </summary>
        [Fact]
        public void ResultCollectionTry_Exception()
        {
            const int initialValue = 0;

            var resultCollection = ResultCollectionTry(() => DivisionCollection(initialValue), Exceptions.ExceptionError());

            Assert.True(resultCollection.HasErrors);
            Assert.Equal(ErrorResultType.DevideByZero, resultCollection.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Результирующий ответ с коллекцией без ошибки и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionTryOk_Ok_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = new ResultCollection<int>(initialNumbers);

            var numberAfterTry = numbersResult.ResultCollectionTryOk(DivisionByCollection, CreateErrorTest());

            Assert.True(numberAfterTry.OkStatus);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numberAfterTry.Value));
        }

        /// <summary>
        /// Результирующий ответ с коллекцией с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionTryOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = new ResultCollection<int>(initialError);

            var numberAfterTry = numbersResult.ResultCollectionTryOk(DivisionByCollection, CreateErrorTest());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ с коллекцией и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionTryOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numberResult = new ResultCollection<int>(initialNumbers);

            var numberAfterTry = numberResult.ResultCollectionTryOk(DivisionCollectionByZero, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.Equal(ErrorResultType.DevideByZero, numberAfterTry.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Результирующий ответ с коллекцией с ошибкой и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionTryOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultCollection<int>(initialError);

            var numberAfterTry = numberResult.ResultCollectionTryOk(DivisionCollectionByZero, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}