using BoutiqueCommon.Infrastructure.Implementation.Validation;
using Xunit;

namespace BoutiqueCommonXUnit.Infrastructure.Validation
{
    /// <summary>
    /// Проверка на пустые строки. Тесты
    /// </summary>
    public class EmptyValidationTest
    {
        [Theory]
        [InlineData("test", true)]
        [InlineData("", false)]
        public void EmailValidate(string line, bool validation)
        {
            bool isValid = EmptyValidation.IsValid(line);

            Assert.Equal(validation, isValid);
        }
    }
}