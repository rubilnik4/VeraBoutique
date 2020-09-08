using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Методы расширения для результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultValueTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Вернуть результирующий ответ задачи-объекта с коллекцией без ошибок
        /// </summary>      
        [Fact]
        public async Task ToResultCollectionTaskAsync_Enumerable_OkStatus()
        {
            var collection = Enumerable.Range(0, 3).ToList().AsReadOnly();
            var resultNoError = Task.FromResult((IResultValue<IEnumerable<int>>)new ResultValue<IEnumerable<int>>(collection));

            var resultValue = await resultNoError.ToResultCollectionTaskAsync();

            Assert.True(resultValue.OkStatus);
            Assert.True(collection.SequenceEqual(resultValue.Value));
        }

        /// <summary>
        /// Вернуть результирующий ответ задачи-объекта со значением с ошибкой
        /// </summary>      
        [Fact]
        public async Task ToResultCollectionTaskAsync_Enumerable_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = Task.FromResult((IResultValue<IEnumerable<int>>)new ResultValue<IEnumerable<int>>(error));

            var resultValue = await resultHasError.ToResultCollectionTaskAsync();

            Assert.True(resultValue.HasErrors);
            Assert.Single(resultValue.Errors);
            Assert.True(error.Equals(resultValue.Errors.Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ задачи-объекта с коллекцией без ошибок
        /// </summary>      
        [Fact]
        public async Task ToResultCollectionTaskAsync_IReadOnlyCollection_OkStatus()
        {
            var collection = Enumerable.Range(0, 3).ToList().AsReadOnly();
            var resultNoError = Task.FromResult((IResultValue<IReadOnlyCollection<int>>)new ResultValue<IReadOnlyCollection<int>>(collection));

            var resultValue = await resultNoError.ToResultCollectionTaskAsync();

            Assert.True(resultValue.OkStatus);
            Assert.True(collection.SequenceEqual(resultValue.Value));
        }

        /// <summary>
        /// Вернуть результирующий ответ задачи-объекта со значением с ошибкой
        /// </summary>      
        [Fact]
        public async Task ToResultCollectionTaskAsync_IReadOnlyCollection_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = Task.FromResult((IResultValue<IReadOnlyCollection<int>>)new ResultValue<IReadOnlyCollection<int>>(error));

            var resultValue = await resultHasError.ToResultCollectionTaskAsync();

            Assert.True(resultValue.HasErrors);
            Assert.Single(resultValue.Errors);
            Assert.True(error.Equals(resultValue.Errors.Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ задачи-объекта с коллекцией без ошибок
        /// </summary>      
        [Fact]
        public async Task ToResultCollectionTaskAsync_ReadOnlyCollection_OkStatus()
        {
            var collection = Enumerable.Range(0, 3).ToList().AsReadOnly();
            var resultNoError = Task.FromResult((IResultValue<ReadOnlyCollection<int>>)new ResultValue<ReadOnlyCollection<int>>(collection));

            var resultValue = await resultNoError.ToResultCollectionTaskAsync();

            Assert.True(resultValue.OkStatus);
            Assert.True(collection.SequenceEqual(resultValue.Value));
        }

        /// <summary>
        /// Вернуть результирующий ответ задачи-объекта со значением с ошибкой
        /// </summary>      
        [Fact]
        public async Task ToResultCollectionTaskAsync_ReadOnlyCollection_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = Task.FromResult((IResultValue<ReadOnlyCollection<int>>)new ResultValue<ReadOnlyCollection<int>>(error));

            var resultValue = await resultHasError.ToResultCollectionTaskAsync();

            Assert.True(resultValue.HasErrors);
            Assert.Single(resultValue.Errors);
            Assert.True(error.Equals(resultValue.Errors.Last()));
        }

        /// <summary>
        /// Проверить объект на нул для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckTaskAsync_Ok()
        {
            var initialString = Task.FromResult("NotNull");

            var resultString = await initialString.ToResultValueNullCheckTaskAsync(CreateErrorTest());

            Assert.True(resultString.OkStatus);
            Assert.Equal(initialString.Result, resultString.Value);
        }

        /// <summary>
        /// Проверить объект на нул для задачи-объекта. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckTaskAsync_ErrorNull()
        {
            var initialString = Task.FromResult<string>(null);
            var initialError = CreateErrorTest();
            var resultString = await initialString.ToResultValueNullCheckTaskAsync(initialError);

            Assert.True(resultString.HasErrors);
            Assert.True(resultString.Errors.First().Equals(initialError));
        }
    }
}