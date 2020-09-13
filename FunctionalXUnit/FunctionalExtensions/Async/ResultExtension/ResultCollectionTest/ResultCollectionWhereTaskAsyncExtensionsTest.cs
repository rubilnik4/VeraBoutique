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
    /// Обработка условий для результирующего ответа задачи-объекта с коллекцией. Тесты
    /// </summary>
    public class ResultCollectionWhereTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе задачи-объекта с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(initialCollection));

            var resultAfterWhere =
                await resultCollection.ResultCollectionContinueTaskAsync(numbers => true,
                                                okFunc: CollectionToString,
                                                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueTaskAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(initialCollection));

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere =
                await resultCollection.ResultCollectionContinueTaskAsync(number => false,
                                                          okFunc: _ => new List<string> { String.Empty },
                                                          badFunc: number => errorsBad);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(errorInitial));

            var resultAfterWhere =
                await resultCollection.ResultCollectionContinueTaskAsync(number => true,
                                                          okFunc: _ =>new List<string> { String.Empty },
                                                          badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueTaskAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(errorsInitial));

            var resultAfterWhere =
                await resultCollection.ResultCollectionContinueTaskAsync(number => false,
                                                          okFunc: _ => new List<string> { String.Empty },
                                                          badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(initialCollection));

            var resultAfterWhere =
                await resultCollection.ResultCollectionOkBadTaskAsync(
                    okFunc: CollectionToString,
                    badFunc: _ => new List<string> { String.Empty });

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadTaskAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(errorsInitial));

            var resultAfterWhere =
                await resultCollection.ResultCollectionOkBadTaskAsync(
                    okFunc: _ => new List<string> { String.Empty },
                    badFunc: errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Single(resultAfterWhere.Value);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(initialCollection));

            var resultAfterWhere = await resultCollection.ResultCollectionOkTaskAsync(CollectionToString);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkTaskAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(errorInitial));

            var resultAfterWhere = await resultValue.ResultCollectionOkTaskAsync(CollectionToString);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBadTaskAsync_Ok_ReturnInitial()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(initialCollection));

            var resultAfterWhere =
                await resultCollection.ResultCollectionBadTaskAsync(errors => new List<int> { errors.Count });

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialCollection, resultAfterWhere.Value);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBadTaskAsync_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(errorsInitial));

            var resultAfterWhere = 
                await resultValue.ResultCollectionBadTaskAsync(errors => new List<int> { errors.Count });

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value.First());
        }
    }
}