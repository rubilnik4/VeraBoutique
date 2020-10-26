using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueMVC.Extensions.Controllers.Sync;
using Xunit;

namespace MVCXUnit.Extensions.Controllers.Sync
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
            var errorInitial = ErrorData.ErrorsTest;
            var modelState = errorInitial.ToModelState();

            Assert.False(modelState.IsValid);
            Assert.Equal(errorInitial.Count, modelState.ErrorCount);
            Assert.Equal(errorInitial.First().ErrorResultType.ToString(), modelState.Keys.First());
        }
    }
}