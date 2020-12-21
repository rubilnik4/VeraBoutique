using BoutiqueCommon.Extensions.StringExtensions;
using Xunit;

namespace BoutiqueCommonXUnit.Extensions.StringExtensions
{
    /// <summary>
    /// Методы расширения для строк. Тесты
    /// </summary>
    public class StringExtensionsTest
    {
        [Theory]
        [InlineData("MainController", "controller", "Main")]
        [InlineData("ControllerMain", "controller", "Main")]
        [InlineData("MainControllerMain", "controller", "MainMain")]
        public void SubstringRemoveTest(string stringIn, string stringRemove, string stringResult)
        {
            string stringAfterRemove = stringIn.SubstringRemove(stringRemove);
            
            Assert.Equal(stringResult, stringAfterRemove);
        }
    }
}