using System.Linq;
using Functional.Models.Enums;
using FunctionalXUnit.Data;
using FunctionalXUnit.Mocks.Implementation;
using Xunit;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultError.ResultErrorTryExtensions;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultErrorTest
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
            int initialValue = Numbers.Number;
            var resultError = ResultErrorTry(() => SyncFunctions.Division(initialValue), Exceptions.ExceptionError());

            Assert.True(resultError.OkStatus);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void ResultErrorTry_Exception()
        {
            const int initialValue = 0;
            var resultError = ResultErrorTry(() => SyncFunctions.Division(initialValue), Exceptions.ExceptionError());

            Assert.True(resultError.HasErrors);
            Assert.Equal(ErrorResultType.DivideByZero, resultError.Errors.First().ErrorResultType);
        }
    }
}