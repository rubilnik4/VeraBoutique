using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using System.Linq;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultError
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа. Тест
    /// </summary>
    public class ResultErrorBindWhereExtensionsTest
    {
        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public void ResultErrorBindOk_Ok_NoError()
        {
            var initialResult = new Functional.Models.Implementations.Result.ResultError();
            var addingResult = new Functional.Models.Implementations.Result.ResultError();

            var result = initialResult.ResultErrorBindOk(() => addingResult);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта с ошибкой
        /// </summary>
        [Fact]
        public void ResultErrorBindOk_Ok_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = new Functional.Models.Implementations.Result.ResultError();
            var addingResult = new Functional.Models.Implementations.Result.ResultError(initialError);

            var result = initialResult.ResultErrorBindOk(() => addingResult);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(initialError));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public void ResultErrorBindOk_Bad_NoError()
        {
            var initialError = CreateErrorTest();
            var initialResult = new Functional.Models.Implementations.Result.ResultError(initialError);
            var addingResult = new Functional.Models.Implementations.Result.ResultError();

            var result = initialResult.ResultErrorBindOk(() => addingResult);

            Assert.True(result.HasErrors);
            Assert.True(result.Equals(initialResult));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public void ResultErrorBindOk_Bad_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = new Functional.Models.Implementations.Result.ResultError(initialError);
            var addingResult = new Functional.Models.Implementations.Result.ResultError(initialError);

            var result = initialResult.ResultErrorBindOk(() => addingResult);

            Assert.True(result.HasErrors);
            Assert.Single(result.Errors);
            Assert.True(result.Equals(initialResult));
        }
    }
}