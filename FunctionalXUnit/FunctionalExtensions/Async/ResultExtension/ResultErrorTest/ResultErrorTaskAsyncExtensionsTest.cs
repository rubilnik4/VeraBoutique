using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.Models.Implementations.Result;
using Functional.Models.Implementations.ResultFactory;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultErrorTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа для задачи-объекта. Тесты
    /// </summary>
    public class ResultErrorTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public async Task ToResultBindValue_OkStatus()
        {
            var resultNoError = ResultErrorFactory.CreateTaskResultError();
            const string value = "OkStatus";
            var resultValue = new ResultValue<string>(value);

            var resultValueAfter = await resultNoError.ToResultBindValueTaskAsync(resultValue);

            Assert.True(resultValueAfter.OkStatus);
            Assert.Equal(value, resultValueAfter.Value);
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public async Task ToResultBindValue_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = ResultErrorFactory.CreateTaskResultError(error);
            const string value = "BadStatus";
            var resultValue = new ResultValue<string>(value);

            var resultValueAfter = await resultHasError.ToResultBindValueTaskAsync(resultValue);

            Assert.True(resultValueAfter.HasErrors);
            Assert.Single(resultValueAfter.Errors);
            Assert.True(error.Equals(resultValueAfter.Errors.Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой в результирующем ответе
        /// </summary>      
        [Fact]
        public async Task ToResultBindValue_HasErrorsBind()
        {
            var resultNoError = ResultErrorFactory.CreateTaskResultError();
            var error = CreateErrorTest();
            var resultValue = new ResultValue<string>(error);

            var resultValueAfter = await resultNoError.ToResultBindValueTaskAsync(resultValue);

            Assert.True(resultValueAfter.HasErrors);
            Assert.Single(resultValueAfter.Errors);
            Assert.True(error.Equals(resultValueAfter.Errors.Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public async Task ToResultValueBind_HasErrorsBindInitial()
        {
            var error = CreateErrorTest();
            var resultHasError = ResultErrorFactory.CreateTaskResultError(error);
            var errors = CreateErrorListTwoTest();
            var resultValue = new ResultValue<string>(errors);

            var resultValueAfter = await resultHasError.ToResultBindValueTaskAsync(resultValue);

            Assert.True(resultValueAfter.HasErrors);
            Assert.Single(resultValueAfter.Errors);
            Assert.True(error.Equals(resultValueAfter.Errors.Last()));
        }
    }
}