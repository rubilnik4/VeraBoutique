using System.Collections.Generic;
using System.Linq;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using FunctionalXUnit.Mocks.Interfaces;
using Moq;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;
using static FunctionalXUnit.Data.Collections;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultCollection
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа с коллекцией. Тесты
    /// </summary>
    public class ResultCollectionBindWhereExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public void ResultCollectionBindOk_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = resultCollection.ResultCollectionBindOk(numbers => new ResultCollection<string>(CollectionToString(numbers)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public void ResultCollectionBindOk_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorInitial);

            var resultAfterWhere = resultCollection.ResultCollectionBindOk(numbers => new ResultCollection<string>(CollectionToString(numbers)));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public void ResultCollectionBindBad_Ok_ReturnInitial()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = resultCollection.ResultCollectionBindBad(errors => new ResultCollection<int>(new List<int> { errors.Count }));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(resultCollection.Value, resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public void ResultCollectionBindBad_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = resultCollection.ResultCollectionBindBad(errors => new ResultCollection<int>(new List<int> { errors.Count }));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией. Без ошибок
        /// </summary>
        [Fact]
        public void ResultCollectionBindErrorsOk_NoError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);
            var resultError = new Functional.Models.Implementations.Result.ResultError();
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>())).
                                Returns(resultError);

            var resultAfterWhere = resultCollection.ResultCollectionBindErrorsOk(numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.Value));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(initialCollection), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией. С ошибками
        /// </summary>
        [Fact]
        public void ResultCollectionBindErrorsOk_HasError()
        {
            var initialCollection = GetRangeNumber();
            var initialError = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(initialCollection);
            var resultError = new Functional.Models.Implementations.Result.ResultError(initialError);
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>())).
                                Returns(resultError);

            var resultAfterWhere = resultCollection.ResultCollectionBindErrorsOk(numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(initialError.Equals(resultAfterWhere.Errors.First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(initialCollection), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией. Без ошибок
        /// </summary>
        [Fact]
        public void ResultCollectionBindErrorsBad_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);
            var resultError = new Functional.Models.Implementations.Result.ResultError();
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>())).
                                Returns(resultError);

            var resultAfterWhere = resultCollection.ResultCollectionBindErrorsOk(numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией. С ошибками
        /// </summary>
        [Fact]
        public void ResultCollectionBindErrorsBad_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialError = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);
            var resultError = new Functional.Models.Implementations.Result.ResultError(initialError);
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>())).
                                Returns(resultError);

            var resultAfterWhere = resultCollection.ResultCollectionBindErrorsOk(numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
        }
    }
}