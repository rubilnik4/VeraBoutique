using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using Functional.Models.Enums;
using Functional.Models.Implementations.ResultFactory;
using FunctionalXUnit.Data;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;
using static FunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
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
            int initialValue = Numbers.Number;
            var numberResult = ResultValueFactory.CreateTaskResultValue(initialValue);

            var numberAfterTry = await numberResult.ResultValueTryOkTaskAsync(Division, CreateErrorTest());

            Assert.True(numberAfterTry.OkStatus);
            Assert.Equal(Division(initialValue), numberAfterTry.Value);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkTaskAsync_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = ResultValueFactory.CreateTaskResultValueError<int>(initialError);

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
            const int initialValue = 0;
            var numberResult = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultValue = await numberResult.ResultValueTryOkTaskAsync(Division, Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkTaskAsync_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = ResultValueFactory.CreateTaskResultValueError<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueTryOkTaskAsync(Division, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}