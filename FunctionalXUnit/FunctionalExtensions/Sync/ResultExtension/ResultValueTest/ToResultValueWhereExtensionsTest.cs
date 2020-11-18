using System.Linq;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Преобразование значения в результирующий ответ с условием. Тесты
    /// </summary>
    public class ToResultValueWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhere_Ok()
        {
            const int number = 1;

            var result = number.ToResultValueWhere(_ => true,
                                                   _ => CreateErrorTest());

            Assert.True(result.OkStatus);
            Assert.Equal(number, result.Value);
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhere_BadError()
        {
            const int number = 1;
            var errorInitial = CreateErrorTest();
            var result = number.ToResultValueWhere(_ => false,
                                                   _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }
    }
}