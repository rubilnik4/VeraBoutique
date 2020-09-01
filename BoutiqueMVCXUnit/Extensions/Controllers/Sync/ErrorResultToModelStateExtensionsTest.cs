using BoutiqueMVC.Extensions.Controllers.Sync;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Xunit;
using static BoutiqueMVCXUnit.Data.ErrorData;

namespace BoutiqueMVCXUnit.Extensions.Controllers.Sync
{
    /// <summary>
    /// Методы расширения для ошибок результирующего результата. Тесты
    /// </summary>
    public class ErrorResultToModelStateExtensionsTest
    {
        /// <summary>
        /// Сформировать модель ошибок. Одна ошибка
        /// </summary>
        [Fact]
        public void ToModelState_OneError()
        {
            var errorInitial = CreateErrorListTwoTest();
            var modelState = errorInitial.ToModelState();

            Assert.False(modelState.IsValid);
            Assert.Equal(errorInitial.Count, modelState.ErrorCount);
            Assert.Equal(errorInitial.First().ErrorResultType.ToString(), modelState.Keys.First());
        }
    }
}