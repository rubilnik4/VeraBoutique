using System.Linq;
using Functional.Models.Enums;
using FunctionalXUnit.Data;
using FunctionalXUnit.Mocks.Implementation;
using Xunit;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection.ResultCollectionTryExtensions;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultCollection
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией и обработкой исключений. Тесты
    /// </summary>
    public class ResultCollectionTryExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с коллекцией
        /// </summary>
        [Fact]
        public void ResultCollectionTry_Ok()
        {
            var resultCollection = ResultCollectionTry(() => SyncFunctions.DivisionCollection(1), Exceptions.FuncExceptionToError);

            Assert.True(resultCollection.OkStatus);
            Assert.Equal(SyncFunctions.DivisionCollection(1), resultCollection.Value);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с коллекцией
        /// </summary>
        [Fact]
        public void ResultCollectionTry_Exception()
        {
            var resultCollection = ResultCollectionTry(() => SyncFunctions.DivisionCollection(0), Exceptions.FuncExceptionToError);

            Assert.True(resultCollection.HasErrors);
            Assert.Equal(ErrorResultType.DevideByZero, resultCollection.Errors.First().ErrorResultType);
        }
    }
}