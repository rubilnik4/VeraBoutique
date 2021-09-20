using BoutiqueCommon.Infrastructure.Implementation.Validation;
using Xunit;

namespace BoutiqueCommonXUnit.Infrastructure.Validation
{
    /// <summary>
    /// Проверка почты. Тесты
    /// </summary>
    public class EmailValidationTest
    {
        [Theory]
        [InlineData("rubilnik4@yandex.ru", true)]
        [InlineData("rubilnik4@yandex", false)]
        [InlineData("rubilnik4yandex.ru", false)]
        public void EmailValidate(string email, bool validation)
        {
            bool isValid = EmailValidation.IsValid(email);

            Assert.Equal(validation, isValid);
        }
    }
}