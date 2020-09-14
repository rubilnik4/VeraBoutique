using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.Models.Implementations.Result;
using Functional.Models.Implementations.ResultFactory;
using Functional.Models.Interfaces.Result;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultErrorTest
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа для задачи-объекта. Тест
    /// </summary>
    public class ResultErrorBindWhereTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkTaskAsync_Ok_NoError()
        {
            var initialResult = ResultErrorFactory.CreateTaskResultError();
            var addingResult = new ResultError();

            var result = await initialResult.ResultErrorBindOkTaskAsync(() => addingResult);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkTaskAsync_Ok_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = ResultErrorFactory.CreateTaskResultError();
            var addingResult = new ResultError(initialError);

            var result = await initialResult.ResultErrorBindOkTaskAsync(() => addingResult);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(initialError));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkTaskAsync_Bad_NoError()
        {
            var initialError = CreateErrorTest();
            var initialResult = ResultErrorFactory.CreateTaskResultError(initialError);
            var addingResult = new ResultError();

            var result = await initialResult.ResultErrorBindOkTaskAsync(() => addingResult);

            Assert.True(result.HasErrors);
            Assert.True(result.Equals(initialResult.Result));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkTaskAsync_Bad_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = ResultErrorFactory.CreateTaskResultError(initialError);
            var addingResult =new ResultError(initialError);

            var result = await initialResult.ResultErrorBindOkTaskAsync(() => addingResult);

            Assert.True(result.HasErrors);
            Assert.Single(result.Errors);
            Assert.True(result.Equals(initialResult.Result));
        }
    }
}