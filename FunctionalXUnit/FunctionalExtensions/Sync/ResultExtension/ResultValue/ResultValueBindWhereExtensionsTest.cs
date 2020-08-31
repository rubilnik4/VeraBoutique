using System.Linq;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValue
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа со значением. Тесты
    /// </summary>
    public class ResultValueBindWhereExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void ResultValueBindOk_Ok_ReturnNewValue()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueBindOk(number => new ResultValue<string>(number.ToString()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public void ResultValueBindOk_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere = resultValue.ResultValueBindOk(number => new ResultValue<string>(number.ToString()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void ResultValueBindBad_Ok_ReturnInitial()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueBindBad(errors => new ResultValue<int>(errors.Count));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(resultValue.Value, resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public void ResultValueBindBad_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere = resultValue.ResultValueBindBad(errors => new ResultValue<int>(errors.Count));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value);
        }
    }
}