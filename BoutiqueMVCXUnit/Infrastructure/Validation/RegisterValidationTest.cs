using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueMVC.Infrastructure.Implementation.Validation;
using BoutiqueMVC.Models.Implementations.Identity;
using Xunit;

namespace BoutiqueMVCXUnit.Infrastructure.Validation
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
            var validate = RegisterValidation.RegisterValidate(register, AuthorizeSettings);

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
            var validate = RegisterValidation.RegisterValidate(register, AuthorizeSettings);

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
            var validate = RegisterValidation.RegisterValidate(register, AuthorizeSettings);

            Assert.True(validate.HasErrors);
        }

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        private static AuthorizeSettings AuthorizeSettings =>
            new(8, true, false, true);
    }
}