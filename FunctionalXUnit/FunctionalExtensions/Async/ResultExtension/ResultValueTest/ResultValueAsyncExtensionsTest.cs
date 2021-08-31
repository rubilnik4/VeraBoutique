using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Асинхронные методы расширения для результирующего ответа. Тесты
    /// </summary>
    public class ResultValueAsyncExtensionsTest
    {
        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheck_Ok()
        {
            const string initialString = "NotNull";

            var result = await initialString.ToResultValueNullCheckAsync(CreateErrorTestTask());

            Assert.True(result.OkStatus);
            Assert.Equal(initialString, result.Value);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheck_ErrorNull()
        {
            const string? initialString = null;
            var initialError = CreateErrorTestTask();
            var result = await initialString.ToResultValueNullCheckAsync(initialError);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(initialError.Result));
        }

        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckStruct_Ok()
        {
            int? initialInt = 1;

            var result = await initialInt.ToResultValueNullCheckAsync(CreateErrorTestTask());

            Assert.True(result.OkStatus);
            Assert.Equal(initialInt, result.Value);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckStruct_ErrorNull()
        {
            int? initialInt = null;
            var initialError = CreateErrorTestTask();
            var result = await initialInt.ToResultValueNullCheckAsync(initialError);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(initialError.Result));
        }
    }
}