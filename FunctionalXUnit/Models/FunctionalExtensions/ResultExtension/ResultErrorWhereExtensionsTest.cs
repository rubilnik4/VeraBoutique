using Functional.FunctionalExtensions.ResultExtension;
using Functional.Models.Implementations.Result;
using System.Linq;
using Xunit;
using static FunctionalXUnit.Models.Data.ErrorData;

namespace FunctionalXUnit.Models.FunctionalExtensions.ResultExtension
{
    /// <summary>
    /// Обработка условий для результирующего ответа. Тесты
    /// </summary>
    public class ResultErrorWhereExtensionsTest
    {
        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public void ResultValue_OkStatus()
        {
            var resultNoError = new ResultError();
            const string okStatus = "OkStatus";

            var resultValue = resultNoError.ToResultValue(() => okStatus);

            Assert.True(resultValue.OkStatus);
            Assert.Equal(okStatus, resultValue.Value);
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ResultValue_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = new ResultError(error);
            const string okStatus = "BadStatus";

            var resultValue = resultHasError.ToResultValue(() => okStatus);

            Assert.True(resultValue.HasErrors);
            Assert.Single(resultValue.Errors);
            Assert.True(error.Equals(resultValue.Errors.Last()));
        }
    }
}