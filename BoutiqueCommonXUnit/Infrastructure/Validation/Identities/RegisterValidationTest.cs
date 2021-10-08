using BoutiqueCommon.Infrastructure.Implementation.Validation.Identities;
using BoutiqueCommon.Models.Common.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommonXUnit.Data.Authorize;
using Xunit;

namespace BoutiqueCommonXUnit.Infrastructure.Validation.Identities
{
    /// <summary>
    /// Проверка регистрации
    /// </summary>
    public class RegisterValidationTest
    {
        /// <summary>
        /// Проверить регистрацию
        /// </summary>
        [Fact]
        public void Validate_Ok()
        {
            var authorize = new AuthorizeDomain("rubilnik4@yandex.ru", "password1919");
            var personal = new PersonalDomain("Name", "Surname", "Address", "+79224725777");
            var register = new RegisterDomain(authorize, personal);
            var validate = RegisterValidation.RegisterValidate(register, AuthorizeSettings.PasswordSettings);

            Assert.True(validate.OkStatus);
        }

        /// <summary>
        /// Проверить регистрацию
        /// </summary>
        [Fact]
        public void Validate_ErrorAuthorize()
        {
            var authorize = new AuthorizeDomain("rubilnik4", "password1919");
            var personal = new PersonalDomain("Name", "Surname", "Address", "+79224725777");
            var register = new RegisterDomain(authorize, personal);
            var validate = RegisterValidation.RegisterValidate(register, AuthorizeSettings.PasswordSettings);

            Assert.True(validate.HasErrors);
        }

        /// <summary>
        /// Проверить регистрацию
        /// </summary>
        [Fact]
        public void Validate_ErrorRegister()
        {
            var authorize = new AuthorizeDomain("rubilnik4@yandex.ru", "password1919");
            var personal = new PersonalDomain("Name", "", "Address", "+79224725777");
            var register = new RegisterDomain(authorize, personal);
            var validate = RegisterValidation.RegisterValidate(register, AuthorizeSettings.PasswordSettings);

            Assert.True(validate.HasErrors);
        }
    }
}