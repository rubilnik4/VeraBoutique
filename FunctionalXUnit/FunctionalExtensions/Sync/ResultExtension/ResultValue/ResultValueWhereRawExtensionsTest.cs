using System.Linq;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValue
{
    /// <summary>
    /// Обработка условий для результирующего ответа со значением в обертке. Тесты
    /// </summary>
    public class ResultValueWhereRawExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки в обертке
        /// </summary>   
        [Fact]
        public void ResultValueOkRaw_Ok_ReturnNewValue()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueOkRaw(result => new ResultValue<string>(result.Value.ToString()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе с ошибкой в обертке
        /// </summary>   
        [Fact]
        public void ResultValueOkRaw_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere = resultValue.ResultValueOkRaw(result => new ResultValue<string>(result.Value.ToString()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе без ошибки в обертке
        /// </summary>   
        [Fact]
        public void ResultValueBadRaw_Ok_ReturnInitial()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueBadRaw(result => new ResultValue<int>(result.Value * 2));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue, resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой в обертке
        /// </summary>   
        [Fact]
        public void ResultValueBadRaw_Bad_ReturnInitial()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere = resultValue.ResultValueBadRaw(result => new ResultValue<int>(result.Errors.Count * 2));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count * 2, resultAfterWhere.Value);
        }
    }
}