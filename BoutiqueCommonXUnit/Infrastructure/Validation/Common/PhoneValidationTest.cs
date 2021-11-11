using BoutiqueCommon.Infrastructure.Implementation.Validation.Common;
using Xunit;

namespace BoutiqueCommonXUnit.Infrastructure.Validation.Common
{
    /// <summary>
    /// Проверка телефона. Тесты
    /// </summary>
    public class PhoneValidationTest
    {
        [Theory]
        [InlineData("+79224725787", true)]
        [InlineData("89224725787", true)]
        [InlineData("+7922472578", false)]
        [InlineData("892247257", false)]
        [InlineData("8922472/787", false)]
        public void IsValid(string phone, bool validation)
        {
            bool isValid = PhoneValidation.IsValid(phone);

            Assert.Equal(validation, isValid);
        }

        [Theory]
        [InlineData("+79224725787", "9224725787")]
        [InlineData("89224725787", "9224725787")]
        public void GetPhoneWithoutCountry(string phone, string phoneValid)
        {
            string newPhone = PhoneValidation.GetPhoneWithoutCountry(phone);

            Assert.Equal(newPhone, phoneValid);
        }
    }
}