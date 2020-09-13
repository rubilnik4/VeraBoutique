using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using FunctionalXUnit.Mocks.Interfaces;
using Moq;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;
using static FunctionalXUnit.Data.Collections;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollection
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа с коллекцией для задачи-объекта. Тесты
    /// </summary>
    public class ResultCollectionBindWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного условия асинхронного результирующего ответа с коллекцией со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindOkBindAsync_Ok_ReturnNewValue()
        {
            var numberCollection = GetRangeNumber();
            var resultCollection = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(numberCollection));

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkBindAsync(
                numbers => Task.FromResult((IResultCollection<string>)new ResultCollection<string>(CollectionToString(numbers))));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(await CollectionToStringAsync(numberCollection), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия асинхронного результирующего ответа с коллекцией со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindOkBindAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(errorInitial));

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkBindAsync(
                numbers => Task.FromResult((IResultCollection<string>)new ResultCollection<string>(CollectionToString(numbers))));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение негативного условия асинхронного результирующего ответа с коллекцией со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindBadBindAsync_Ok_ReturnInitial()
        {
            var numberCollection = GetRangeNumber();
            var resultCollection = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(numberCollection));

            var resultAfterWhere = await resultCollection.ResultCollectionBindBadBindAsync(
                errors => Task.FromResult((IResultCollection<int>)new ResultCollection<int>(new List<int> { errors.Count })));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(numberCollection.SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение негативного условия асинхронного результирующего ответа с коллекцией со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindBadBindAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(errorsInitial));

            var resultAfterWhere = await resultCollection.ResultCollectionBindBadBindAsync(
                errors => Task.FromResult((IResultCollection<int>)new ResultCollection<int>(new List<int> { errors.Count })));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsOkBindAsync_NoError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(initialCollection));
            var resultError = (IResultError)new Functional.Models.Implementations.Result.ResultError();
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>())).
                                ReturnsAsync(resultError);

            var resultAfterWhere = await resultCollection.ResultCollectionBindErrorsOkBindAsync(
                                        numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.Value));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsOkBindAsync_HasError()
        {
            var initialCollection = GetRangeNumber();
            var initialError = CreateErrorTest();
            var resultCollection = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(initialCollection));
            var resultError = (IResultError)new Functional.Models.Implementations.Result.ResultError(initialError);
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>())).
                                ReturnsAsync(resultError);

            var resultAfterWhere = await resultCollection.ResultCollectionBindErrorsOkBindAsync(
                                        number => resultFunctionsMock.Object.NumbersToResultAsync(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(initialError.Equals(resultAfterWhere.Errors.First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией и ошибкой для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsBadBindAsync_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(errorsInitial));
            var resultError = (IResultError)new Functional.Models.Implementations.Result.ResultError();
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>())).
                                ReturnsAsync(resultError);

            var resultAfterWhere =
                await resultValue.ResultCollectionBindErrorsOkBindAsync(number => resultFunctionsMock.Object.NumbersToResultAsync(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией и ошибкой для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsBadBindAsync_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialErrorToAdd = CreateErrorTest();
            var resultValue = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(errorsInitial));
            var resultError = (IResultError)new Functional.Models.Implementations.Result.ResultError(initialErrorToAdd);
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>())).
                                ReturnsAsync(resultError);

            var resultAfterWhere =
                await resultValue.ResultCollectionBindErrorsOkBindAsync(number => resultFunctionsMock.Object.NumbersToResultAsync(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
        }
    }
}