using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.Models.Interfaces.Result;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultErrorTest
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа. Тест
    /// </summary>
    public class ResultErrorBindWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkAsync_Ok_NoError()
        {
            var initialResult = new Functional.Models.Implementations.Result.ResultError();
            var addingResult = (IResultError)new Functional.Models.Implementations.Result.ResultError();

            var result = await initialResult.ResultErrorBindOkAsync(() => Task.FromResult(addingResult));

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkAsync_Ok_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = new Functional.Models.Implementations.Result.ResultError();
            var addingResult = (IResultError)new Functional.Models.Implementations.Result.ResultError(initialError);

            var result = await initialResult.ResultErrorBindOkAsync(() => Task.FromResult(addingResult));

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(initialError));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkAsync_Bad_NoError()
        {
            var initialError = CreateErrorTest();
            var initialResult = new Functional.Models.Implementations.Result.ResultError(initialError);
            var addingResult = (IResultError)new Functional.Models.Implementations.Result.ResultError();

            var result = await initialResult.ResultErrorBindOkAsync(() => Task.FromResult(addingResult));

            Assert.True(result.HasErrors);
            Assert.True(result.Equals(initialResult));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkAsync_Bad_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = new Functional.Models.Implementations.Result.ResultError(initialError);
            var addingResult = (IResultError)new Functional.Models.Implementations.Result.ResultError(initialError);

            var result = await initialResult.ResultErrorBindOkAsync(() => Task.FromResult(addingResult));

            Assert.True(result.HasErrors);
            Assert.Single(result.Errors);
            Assert.True(result.Equals(initialResult));
        }
    }
}