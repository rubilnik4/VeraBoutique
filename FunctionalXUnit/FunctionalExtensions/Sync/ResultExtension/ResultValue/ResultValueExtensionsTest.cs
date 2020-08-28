using System.Collections.Generic;
using System.Linq;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValue
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultValueExtensionsTest
    {
        /// <summary>
        /// Вернуть результирующий ответ с коллекцией без ошибок
        /// </summary>      
        [Fact]
        public void ToResultCollection_OkStatus()
        {
            var collection = Enumerable.Range(0, 3).ToList().AsReadOnly();
            var resultNoError = new ResultValue<IEnumerable<int>>(collection);

            var resultValue = resultNoError.ToResultCollection();

            Assert.True(resultValue.OkStatus);
            Assert.True(collection.SequenceEqual(resultValue.Value));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ToResultValue_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = new ResultValue<IEnumerable<int>>(error);

            var resultValue = resultHasError.ToResultCollection();

            Assert.True(resultValue.HasErrors);
            Assert.Single(resultValue.Errors);
            Assert.True(error.Equals(resultValue.Errors.Last()));
        }
    }
}