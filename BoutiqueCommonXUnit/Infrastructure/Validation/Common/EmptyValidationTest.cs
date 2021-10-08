using BoutiqueCommon.Infrastructure.Implementation.Validation.Common;
using Xunit;

namespace BoutiqueCommonXUnit.Infrastructure.Validation.Common
{
    /// <summary>
    /// Проверка на пустые строки. Тесты
    /// </summary>
    public class EmptyValidationTest
    {
        [Theory]
        [InlineData("test", true)]
        [InlineData("", false)]
        public void IsValid(string line, bool validation)
        {
            bool isValid = EmptyValidation.IsValid(line);

            Assert.Equal(validation, isValid);
        }
    }
}