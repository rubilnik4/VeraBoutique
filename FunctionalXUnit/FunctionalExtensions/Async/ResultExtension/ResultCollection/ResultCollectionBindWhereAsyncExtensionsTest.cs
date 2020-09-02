using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using FunctionalXUnit.Data;
using FunctionalXUnit.Mocks.Interfaces;
using Moq;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;
using static FunctionalXUnit.Data.Collections;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollection
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа со значением. Тесты
    /// </summary>
    public class ResultCollectionBindWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного условия результирующего асинхронного ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindOkAsync_Ok_ReturnNewValue()
        {
            var numberCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(numberCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkAsync(
                numbers => Task.FromResult((IResultCollection<string>)new ResultCollection<string>(CollectionToString(numbers))));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(await CollectionToStringAsync(numberCollection), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия результирующего асинхронного ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindOkAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkAsync(
                numbers => Task.FromResult((IResultCollection<string>)new ResultCollection<string>(CollectionToString(numbers))));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего асинхронного ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindBadAsync_Ok_ReturnInitial()
        {
            var numberCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(numberCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBindBadAsync(
                errors => Task.FromResult((IResultCollection<int>)new ResultCollection<int>(new List<int> { errors.Count })));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(numberCollection.SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего асинхронного ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindBadAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindBadAsync(
                errors => Task.FromResult((IResultCollection<int>)new ResultCollection<int>(new List<int> { errors.Count })));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsOkAsync_NoError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);
            var resultError = (IResultError)new Functional.Models.Implementations.Result.ResultError();
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>())).
                                ReturnsAsync(resultError);

            var resultAfterWhere =
                await resultCollection.ResultCollectionBindErrorsOkAsync(numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.Value));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsOkAsync_HasError()
        {
            var initialCollection = GetRangeNumber();
            var initialError = CreateErrorTest();
            var resultValue = new ResultCollection<int>(initialCollection);
            var resultError = (IResultError)new Functional.Models.Implementations.Result.ResultError(initialError);
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>())).
                                ReturnsAsync(resultError);

            var resultAfterWhere =
                await resultValue.ResultValueBindErrorsOkAsync(numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(initialError.Equals(resultAfterWhere.Errors.First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией и ошибкой. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsBadAsync_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultCollection<int>(errorsInitial);
            var resultError = (IResultError)new Functional.Models.Implementations.Result.ResultError();
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>())).
                                ReturnsAsync(resultError);

            var resultAfterWhere =
                await resultValue.ResultValueBindErrorsOkAsync(numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией и ошибкой. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsBadAsync_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialError = CreateErrorTest();
            var resultValue = new ResultCollection<int>(errorsInitial);
            var resultError = (IResultError)new Functional.Models.Implementations.Result.ResultError(initialError);
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>())).
                                ReturnsAsync(resultError);

            var resultAfterWhere =
                await resultValue.ResultValueBindErrorsOkAsync(numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Never);
        }
    }
}