using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;
using static FunctionalXUnit.Data.Collections;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollection
{
    /// <summary>
    /// Обработка асинхронных условий для результирующего ответа с коллекцией. Тесты
    /// </summary>
    public class ResultCollectionWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere =
                await resultCollection.ResultCollectionContinueAsync(numbers => true,
                                                okFunc: CollectionToStringAsync,
                                                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere =
                await resultCollection.ResultCollectionContinueAsync(number => false,
                                                          okFunc: _ => Task.FromResult((IEnumerable<string>)new List<string> { String.Empty }),
                                                          badFunc: number => Task.FromResult((IEnumerable<IErrorResult>)errorsBad));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorInitial);

            var resultAfterWhere =
                await resultCollection.ResultCollectionContinueAsync(number => true,
                                                          okFunc: _ => Task.FromResult((IEnumerable<string>)new List<string> { String.Empty }),
                                                          badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere =
                await resultCollection.ResultCollectionContinueAsync(number => false,
                                                          okFunc: _ => Task.FromResult((IEnumerable<string>)new List<string> { String.Empty }),
                                                          badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение положительного условия в асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere =
                await resultCollection.ResultCollectionOkBadAsync(
                    okFunc: CollectionToStringAsync,
                    badFunc: _ => Task.FromResult((IEnumerable<string>)new List<string> { String.Empty }));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение негативного условия в асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere =
                await resultCollection.ResultCollectionOkBadAsync(
                    okFunc: _ => Task.FromResult((IEnumerable<string>)new List<string> { String.Empty }),
                    badFunc: errors => Task.FromResult((IEnumerable<string>)new List<string> { errors.Count.ToString() }));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Single(resultAfterWhere.Value);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Выполнение положительного условия в асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionOkAsync(CollectionToStringAsync);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultCollection<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultCollectionOkAsync(CollectionToStringAsync);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBadAsync_Ok_ReturnInitial()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere =
                await resultCollection.ResultCollectionBadAsync(errors => Task.FromResult((IEnumerable<int>)new List<int> { errors.Count }));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialCollection, resultAfterWhere.Value);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBadAsync_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = 
                await resultValue.ResultCollectionBadAsync(errors => Task.FromResult((IEnumerable<int>)new List<int> { errors.Count }));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value.First());
        }
    }
}