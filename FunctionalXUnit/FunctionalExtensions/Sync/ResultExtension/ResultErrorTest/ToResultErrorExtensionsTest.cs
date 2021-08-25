using System.Collections.Generic;
using System.Linq;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Results;
using FunctionalXUnit.Data;
using Xunit;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultErrorTest
{
    /// <summary>
    /// Преобразование в результирующий ответ. Тесты
    /// </summary>
    public class ToResultErrorExtensionsTest
    {
        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>
        [Fact]
        public void ToResultError_Ok()
        {
            var results = new List<IResultError>
            {
                new ResultError(ErrorData.CreateErrorListTwoTest()), 
                new ResultError(ErrorData.CreateErrorTest())
            };

            var result = results.ToResultError();

            Assert.True(result.Errors.SequenceEqual(results.SelectMany(resultError => resultError.Errors)));
        }
    }
}