using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using FunctionalXUnit.Data;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;
using static FunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений для задачи-объекта. Тесты
    /// </summary>
    public class ResultValueTryTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkTaskAsync_OkResult_OkTry()
        {
            const int initialNumber = 2;
            var numberResult = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialNumber));

            var numberAfterTry = await numberResult.ResultValueTryOkTaskAsync(Division, CreateErrorTest());

            Assert.True(numberAfterTry.OkStatus);
            Assert.Equal(Division(initialNumber), numberAfterTry.Value);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkTaskAsync_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialError));

            var numberAfterTry = await numberResult.ResultValueTryOkTaskAsync(Division, CreateErrorTest());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkTaskAsync_OkResult_ExceptionTry()
        {
            const int initialNumber = 0;
            var numberResult = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialNumber));

            var numberAfterTry = await numberResult.ResultValueTryOkTaskAsync(Division, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.Equal(ErrorResultType.DevideByZero, numberAfterTry.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkTaskAsync_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialError));

            var numberAfterTry = await numberResult.ResultValueTryOkTaskAsync(Division, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}