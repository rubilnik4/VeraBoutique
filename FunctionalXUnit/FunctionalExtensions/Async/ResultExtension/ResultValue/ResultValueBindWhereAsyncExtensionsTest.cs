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
    /// Обработка условий для асинхронного результирующего связывающего ответа со значением. Тесты
    /// </summary>
    public class ResultCollectionBindWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного условия результирующего асинхронного ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultValueBindOkAsync_Ok_ReturnNewValue()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindOkAsync(
                number => Task.FromResult((IResultValue<string>)new ResultValue<string>(number.ToString())));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия результирующего асинхронного ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultValueBindOkAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindOkAsync(
                number => Task.FromResult((IResultValue<string>)new ResultValue<string>(number.ToString())));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего асинхронного ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultValueBindBadAsync_Ok_ReturnInitial()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindBadAsync(
                errors => Task.FromResult((IResultValue<int>)new ResultValue<int>(errors.Count)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue, resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия результирующего асинхронного ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultValueBindBadAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere = await resultValue.ResultValueBindBadAsync(
                errors => Task.FromResult((IResultValue<int>)new ResultValue<int>(errors.Count)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsOkAsync_NoError()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);
            var resultError = (IResultError)new Functional.Models.Implementations.Result.ResultError();
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>())).
                                ReturnsAsync(resultError);

            var resultAfterWhere =
                await resultValue.ResultValueBindErrorsOkAsync(number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue, resultAfterWhere.Value);
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResultAsync(initialValue), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsOkAsync_HasError()
        {
            const int initialValue = 2;
            var initialError = CreateErrorTest();
            var resultValue = new ResultValue<int>(initialValue);
            var resultError = (IResultError)new Functional.Models.Implementations.Result.ResultError(initialError);
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>())).
                                ReturnsAsync(resultError);

            var resultAfterWhere =
                await resultValue.ResultValueBindErrorsOkAsync(number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(initialError.Equals(resultAfterWhere.Errors.First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением и ошибкой. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsBadAsync_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultValue<int>(errorsInitial);
            var resultError = (IResultError)new Functional.Models.Implementations.Result.ResultError();
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>())).
                                ReturnsAsync(resultError);

            var resultAfterWhere =
                await resultValue.ResultValueBindErrorsOkAsync(number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением и ошибкой. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsBadAsync_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialErrorToAdd = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorsInitial);
            var resultError = (IResultError)new Functional.Models.Implementations.Result.ResultError(initialErrorToAdd);
            var resultFunctionsMock = new Mock<IResultFunctions>();
            resultFunctionsMock.Setup(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>())).
                                ReturnsAsync(resultError);

            var resultAfterWhere =
                await resultValue.ResultValueBindErrorsOkAsync(number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>()), Times.Never);
        }
    }
}