using System.Linq;
using Functional.Models.Enums;
using FunctionalXUnit.Data;
using FunctionalXUnit.Mocks.Implementation;
using Xunit;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultError.ResultErrorTryExtensions;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultError
{
    /// <summary>
    /// Методы расширения для результирующего ответа и обработкой исключений. Тесты
    /// </summary>
    public class ResultErrorTryExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ
        /// </summary>
        [Fact]
        public void ResultErrorTry_Ok()
        {
            var resultError = ResultErrorTry(() => SyncFunctions.Division(1), Exceptions.FuncExceptionToError);

            Assert.True(resultError.OkStatus);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void ResultErrorTry_Exception()
        {
            var resultError = ResultErrorTry(() => SyncFunctions.Division(0), Exceptions.FuncExceptionToError);

            Assert.True(resultError.HasErrors);
            Assert.Equal(ErrorResultType.DevideByZero, resultError.Errors.First().ErrorResultType);
        }
    }
}