using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension;
using Functional.FunctionalExtensions.Sync.ResultExtension;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultValueAsyncExtensionsTest
    {
        /// <summary>
        /// Вернуть результирующий ответ задачи-объекта с коллекцией без ошибок
        /// </summary>      
        [Fact]
        public async Task ToResultCollectionTaskAsync_OkStatus()
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
        public async Task ToResultCollectionTaskAsync_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = Task.FromResult((IResultValue<IEnumerable<int>>)new ResultValue<IEnumerable<int>>(error));

            var resultValue = await resultHasError.ToResultCollectionTaskAsync();

            Assert.True(resultValue.HasErrors);
            Assert.Single(resultValue.Errors);
            Assert.True(error.Equals(resultValue.Errors.Last()));
        }
    }
}