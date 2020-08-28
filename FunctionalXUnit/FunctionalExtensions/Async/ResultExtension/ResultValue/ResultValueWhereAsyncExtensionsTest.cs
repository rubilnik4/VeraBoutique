using System;
using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using FunctionalXUnit.Mocks.Implementation;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultValueWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение асинхронного условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public async Task ResultValueContinueAsync_Ok_ReturnNewValue()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere =
                await resultValue.ResultValueContinueAsync(number => true,
                                                           okFunc: AsyncFunctions.IntToStringAsync,
                                                           badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueContinueAsync_Ok_ReturnNewError()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere =
                await resultValue.ResultValueContinueAsync(number => false,
                                                           okFunc: _ => Task.FromResult(String.Empty),
                                                           badFunc: number => Task.FromResult(errorsBad));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.Errors));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueContinueAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere =
                await resultValue.ResultValueContinueAsync(number => true,
                                                           okFunc: _ => Task.FromResult(String.Empty),
                                                           badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueContinueAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere =
                await resultValue.ResultValueContinueAsync(number => false,
                                                           okFunc: _ => Task.FromResult(String.Empty),
                                                           badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение асинхронного положительного условия в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultValueOkBadAsync_Ok_ReturnNewValue()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere =
                await resultValue.ResultValueOkBadAsync(
                    okFunc: AsyncFunctions.IntToStringAsync,
                    badFunc: _ => Task.FromResult(String.Empty));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение асинхронного негативного условия в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultValueOkBadAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere =
                await resultValue.ResultValueOkBadAsync(
                    okFunc: _ => Task.FromResult(String.Empty),
                    badFunc: errors => AsyncFunctions.IntToStringAsync(errors.Count));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение асинхронного положительного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultValueOkAsync_Ok_ReturnNewValue()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = await resultValue.ResultValueOkAsync(AsyncFunctions.IntToStringAsync);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultValueOkAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueOkAsync(AsyncFunctions.IntToStringAsync);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение асинхронного отрицательного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultValueBadAsync_Ok_ReturnInitial()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBadAsync(errors => Task.FromResult(errors.Count));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(resultValue.Value, resultAfterWhere.Value);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultValueBadAsync_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere = await resultValue.ResultValueBadAsync(errors => Task.FromResult(errors.Count));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value);
        }
    }
}