using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using FunctionalXUnit.Mocks.Interfaces;
using Moq;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа со значением для задачи-объекта. Тесты
    /// </summary>
    public class ResultCollectionBindWhereTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultValueBindOkTaskAsync_Ok_ReturnNewValue()
        {
            const int initialValue = 2;
            var resultValue = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialValue));

            var resultAfterWhere = await resultValue.ResultValueBindOkTaskAsync(number => new ResultValue<string>(number.ToString()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultValueBindOkTaskAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = Task.FromResult((IResultValue<int>)new ResultValue<int>(errorInitial));

            var resultAfterWhere = await resultValue.ResultValueBindOkTaskAsync(number => new ResultValue<string>(number.ToString()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultValueBindBadTaskAsync_Ok_ReturnInitial()
        {
            const int initialValue = 2;
            var resultValue = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialValue));

            var resultAfterWhere = await resultValue.ResultValueBindBadTaskAsync(errors => new ResultValue<int>(errors.Count));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue, resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultValueBindBadTaskAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = Task.FromResult((IResultValue<int>)new ResultValue<int>(errorsInitial));

            var resultAfterWhere = await resultValue.ResultValueBindBadTaskAsync(errors => new ResultValue<int>(errors.Count));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsOkTaskAsync_NoError()
        {
            const int initialValue = 2;
            var resultValue = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialValue));
            var resultError = (IResultError)new Functional.Models.Implementations.Result.ResultError();
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>())).
                                Returns(resultError);

            var resultAfterWhere =
                await resultValue.ResultValueBindErrorsOkTaskAsync(number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue, resultAfterWhere.Value);
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(initialValue), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsOkTaskAsync_HasError()
        {
            const int initialValue = 2;
            var initialError = CreateErrorTest();
            var resultValue = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialValue));
            var resultError = (IResultError)new Functional.Models.Implementations.Result.ResultError(initialError);
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>())).
                                Returns(resultError);

            var resultAfterWhere =
                await resultValue.ResultValueBindErrorsOkTaskAsync(number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(initialError.Equals(resultAfterWhere.Errors.First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением и ошибкой для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsBadTaskAsync_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = Task.FromResult((IResultValue<int>)new ResultValue<int>(errorsInitial));
            var resultError = (IResultError)new Functional.Models.Implementations.Result.ResultError();
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>())).
                                Returns(resultError);

            var resultAfterWhere =
                await resultValue.ResultValueBindErrorsOkTaskAsync(number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением и ошибкой для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsBadTaskAsync_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialErrorToAdd = CreateErrorTest();
            var resultValue = Task.FromResult((IResultValue<int>)new ResultValue<int>(errorsInitial));
            var resultError = (IResultError)new Functional.Models.Implementations.Result.ResultError(initialErrorToAdd);
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>())).
                                Returns(resultError);

            var resultAfterWhere =
                await resultValue.ResultValueBindErrorsOkTaskAsync(number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Never);
        }
    }
}