using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа со значением. Тесты
    /// </summary>
    public class ResultValueBindWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного условия результирующего асинхронного ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultValueBindOkAsync_Ok_ReturnNewValue()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindOkAsync(
                number => Task.FromResult((IResultValue<string>)new ResultValue<string>(number.ToString())));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия результирующего асинхронного ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultValueBindOkAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindOkAsync(
                number => Task.FromResult((IResultValue<string>)new ResultValue<string>(number.ToString())));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего асинхронного ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultValueBindBadAsync_Ok_ReturnInitial()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindBadAsync(
                errors => Task.FromResult((IResultValue<int>)new ResultValue<int>(errors.Count)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(resultValue.Value, resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия результирующего асинхронного ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultValueBindBadAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere = await resultValue.ResultValueBindBadAsync(
                errors => Task.FromResult((IResultValue<int>)new ResultValue<int>(errors.Count)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value);
        }
    }
}