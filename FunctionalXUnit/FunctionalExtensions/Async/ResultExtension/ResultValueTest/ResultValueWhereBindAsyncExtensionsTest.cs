using System;
using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using FunctionalXUnit.Mocks.Implementation;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего ответа со значением задачей-объектом. Тесты
    /// </summary>
    public class ResultValueWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе задачей-объектом
        /// </summary>
        [Fact]
        public async Task ResultValueContinueBindAsync_Ok_ReturnNewValue()
        {
            const int initialValue = 2;
            var resultValueTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialValue));

            var resultAfterWhere =
                await resultValueTask.ResultValueContinueBindAsync(number => true,
                                                                   okFunc: AsyncFunctions.IntToStringAsync,
                                                                   badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном асинхронном результирующем ответе задачи-объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueContinueBindAsync_Ok_ReturnNewError()
        {
            const int initialValue = 2;
            var resultValueTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialValue));

            var errorsBad = CreateErrorListTwoTestTask();
            var resultAfterWhere =
                await resultValueTask.ResultValueContinueBindAsync(number => false,
                                                                   okFunc: _ => Task.FromResult(String.Empty),
                                                                   badFunc: number => errorsBad);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsBad.Result.SequenceEqual(resultAfterWhere.Errors));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе задачи-объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueContinueBindAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValueTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(errorInitial));

            var resultAfterWhere =
                await resultValueTask.ResultValueContinueBindAsync(number => true,
                                                                   okFunc: _ => Task.FromResult(String.Empty),
                                                                   badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе задачи-объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueContinueBindAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultValueTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(errorInitial));

            var resultAfterWhere =
                await resultValueTask.ResultValueContinueBindAsync(number => false,
                                                                   okFunc: _ => Task.FromResult(String.Empty),
                                                                   badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение положительного условия в асинхронном результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultValueOkBadBindAsync_Ok_ReturnNewValue()
        {
            const int initialValue = 2;
            var resultValueTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialValue));

            var resultAfterWhere =
                await resultValueTask.ResultValueOkBadBindAsync(
                    okFunc: AsyncFunctions.IntToStringAsync,
                    badFunc: _ => Task.FromResult(String.Empty));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия в асинхронном результирующем ответе задачи-объекта с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultValueOkBadBindAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValueTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(errorsInitial));

            var resultAfterWhere =
                await resultValueTask.ResultValueOkBadBindAsync(
                    okFunc: _ => Task.FromResult(String.Empty),
                    badFunc: errors => Task.FromResult(errors.Count.ToString()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия в асинхронном результирующем ответе задачи-объекта без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultValueOkBindAsync_Ok_ReturnNewValue()
        {
            const int initialValue = 2;
            var resultValueTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialValue));

            var resultAfterWhere = await resultValueTask.ResultValueOkBindAsync(AsyncFunctions.IntToStringAsync);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе задачи-объекта с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultValueOkBindAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValueTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(errorInitial));

            var resultAfterWhere = await resultValueTask.ResultValueOkBindAsync(AsyncFunctions.IntToStringAsync);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в асинхронном результирующем ответе задачи-объекта без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultValueBadBindAsync_Ok_ReturnInitial()
        {
            const int initialValue = 2;
            var resultValueTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialValue));

            var resultAfterWhere = await resultValueTask.ResultValueBadBindAsync(errors => Task.FromResult(errors.Count));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue, resultAfterWhere.Value);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе задачи-объекта с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultValueBadBindAsync_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValueTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(errorsInitial));

            var resultAfterWhere = await resultValueTask.ResultValueBadBindAsync(errors => Task.FromResult(errors.Count));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value);
        }
    }
}