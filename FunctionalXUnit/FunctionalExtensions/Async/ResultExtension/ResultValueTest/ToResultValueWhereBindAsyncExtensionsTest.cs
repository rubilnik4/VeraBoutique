using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Преобразование значения в результирующий ответ с условием для задачи-объекта асинхронно. Тесты
    /// </summary>
    public class ToResultValueWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereAsync_Ok()
        {
            const int number = 1;

            var result = await Task.FromResult(number).ToResultValueWhereBindAsync(_ => true,
                                                                              _ => CreateErrorTestTask());

            Assert.True(result.OkStatus);
            Assert.Equal(number, result.Value);
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereAsync_BadError()
        {
            const int number = 1;
            var errorInitial = CreateErrorTest();

            var result = await Task.FromResult(number).ToResultValueWhereBindAsync(_ => false,
                                                                              _ => Task.FromResult(errorInitial));

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }
    }
}