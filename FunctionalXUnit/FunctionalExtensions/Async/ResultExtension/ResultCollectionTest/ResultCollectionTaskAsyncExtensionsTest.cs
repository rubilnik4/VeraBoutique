using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using FunctionalXUnit.Data;
using Microsoft.VisualBasic;
using Xunit;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollection
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией. Тесты
    /// </summary>
    public class ResultCollectionTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразовать в ответ со значением-коллекцией. Верно
        /// </summary>
        [Fact]
        public async Task ToResultValue_Ok()
        {
            var numbers = Collections.GetRangeNumber();
            var resultCollectionTask = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(numbers));

            var resultValue = await resultCollectionTask.ToResultValue();
            
            Assert.IsAssignableFrom<IResultValue<IReadOnlyCollection<int>>>(resultValue);
        }
    }
}