using System.Collections.Generic;
using System.Linq;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.Models.Implementations.Result;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultErrorTest
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
            var resultNoError = new ResultError();
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
            var resultHasError = new ResultError(error);
            const string value = "BadStatus";

            var resultValue = resultHasError.ToResultValue(value);

            Assert.True(resultValue.HasErrors);
            Assert.Single(resultValue.Errors);
            Assert.True(error.Equals(resultValue.Errors.Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public void ToResultValueBind_OkStatus()
        {
            var resultNoError = new ResultError();
            const string value = "OkStatus";
            var resultValue = new ResultValue<string>(value);

            var resultValueAfter = resultNoError.ToResultValueBind(resultValue);

            Assert.True(resultValueAfter.OkStatus);
            Assert.Equal(value, resultValueAfter.Value);
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ToResultValueBind_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = new ResultError(error);
            const string value = "BadStatus";
            var resultValue = new ResultValue<string>(value);
            
            var resultValueAfter = resultHasError.ToResultValueBind(resultValue);

            Assert.True(resultValueAfter.HasErrors);
            Assert.Single(resultValueAfter.Errors);
            Assert.True(error.Equals(resultValueAfter.Errors.Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой в результирующем ответе
        /// </summary>      
        [Fact]
        public void ToResultValueBind_HasErrorsBind()
        {
            var resultNoError = new ResultError();
            var error = CreateErrorTest();
            var resultValue = new ResultValue<string>(error);

            var resultValueAfter = resultNoError.ToResultValueBind(resultValue);

            Assert.True(resultValueAfter.HasErrors);
            Assert.Single(resultValueAfter.Errors);
            Assert.True(error.Equals(resultValueAfter.Errors.Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ToResultValueBind_HasErrorsBindInitial()
        {
            var error = CreateErrorTest();
            var resultHasError = new ResultError(error);
            var errors = CreateErrorListTwoTest();
            var resultValue = new ResultValue<string>(errors);

            var resultValueAfter = resultHasError.ToResultValueBind(resultValue);

            Assert.True(resultValueAfter.HasErrors);
            Assert.Single(resultValueAfter.Errors);
            Assert.True(error.Equals(resultValueAfter.Errors.Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ с коллекцией без ошибок
        /// </summary>      
        [Fact]
        public void ToResultCollection_OkStatus()
        {
            var resultNoError = new ResultError();
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
            var resultHasError = new ResultError(error);
            var collection = new List<string> { "BadStatus" };

            var resultValue = resultHasError.ToResultValue(collection);

            Assert.True(resultValue.HasErrors);
            Assert.Single(resultValue.Errors);
            Assert.True(error.Equals(resultValue.Errors.Last()));
        }
    }
}