using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension
{
    /// <summary>
    /// Асинхронные методы расширения для результирующего ответа. Тесты
    /// </summary>
    public class ResultErrorAsyncExtensionsTest
    {
        /// <summary>
        /// Вернуть результирующий ответ задачи-объекта со значением без ошибок
        /// </summary>      
        [Fact]
        public async Task ToResultValueTaskAsync_OkStatus()
        {
            var resultNoError = Task.FromResult((IResultError)new ResultError());
            const string value = "OkStatus";

            var resultValue = await resultNoError.ToResultValueTaskAsync(value);

            Assert.True(resultValue.OkStatus);
            Assert.Equal(value, resultValue.Value);
        }

        /// <summary>
        /// Вернуть результирующий ответ задачи-объекта со значением с ошибкой
        /// </summary>      
        [Fact]
        public async Task ToResultValue_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = Task.FromResult((IResultError)new ResultError(error));
            const string value = "BadStatus";

            var resultValue = await resultHasError.ToResultValueTaskAsync(value);

            Assert.True(resultValue.HasErrors);
            Assert.Single(resultValue.Errors);
            Assert.True(error.Equals(resultValue.Errors.Last()));
        }
    }
}