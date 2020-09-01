using System.Collections.Generic;
using System.Linq;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultError
{
    /// <summary>
    /// Методы расширения для результирующего ответа. Тесты
    /// </summary>
    public class ResultErrorExtensionsTest
    {
        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public void ToResultValue_OkStatus()
        {
            var resultNoError = new Functional.Models.Implementations.Result.ResultError();
            const string value = "OkStatus";

            var resultValue = resultNoError.ToResultValue(value);

            Assert.True(resultValue.OkStatus);
            Assert.Equal(value, resultValue.Value);
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ToResultValue_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = new Functional.Models.Implementations.Result.ResultError(error);
            const string value = "BadStatus";

            var resultValue = resultHasError.ToResultValue(value);

            Assert.True(resultValue.HasErrors);
            Assert.Single(resultValue.Errors);
            Assert.True(error.Equals(resultValue.Errors.Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ с коллекцией без ошибок
        /// </summary>      
        [Fact]
        public void ToResultCollection_OkStatus()
        {
            var resultNoError = new Functional.Models.Implementations.Result.ResultError();
            var collection = new List<string> { "OkStatus" };

            var resultValue = resultNoError.ToResultCollection(collection);

            Assert.True(resultValue.OkStatus);
            Assert.True(collection.SequenceEqual(resultValue.Value) );
        }

        /// <summary>
        /// Вернуть результирующий ответ с коллекцией с ошибкой
        /// </summary>      
        [Fact]
        public void ToResultCollection_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = new Functional.Models.Implementations.Result.ResultError(error);
            var collection = new List<string> { "BadStatus" };

            var resultValue = resultHasError.ToResultValue(collection);

            Assert.True(resultValue.HasErrors);
            Assert.Single(resultValue.Errors);
            Assert.True(error.Equals(resultValue.Errors.Last()));
        }
    }
}