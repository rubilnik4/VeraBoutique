using BoutiqueCommon.Infrastructure.Implementation.Validation;
using Xunit;

namespace BoutiqueCommonXUnit.Infrastructure.Validation
{
    /// <summary>
    /// Проверка пароля. Тесты
    /// </summary>
    public class PasswordValidationTest
    {
        [Theory]
        [InlineData("password1", 9, true, true)]
        [InlineData("", 8, true, false)]
        [InlineData("password1", 10, true, false)]
        [InlineData("password", 8, false, true)]
        public void IsValid(string password, int minLength, bool needDigit, bool validation)
        {
            bool isValid = PasswordValidation.IsValid(password, minLength, needDigit);

            Assert.Equal(validation, isValid);
        }
    }
}