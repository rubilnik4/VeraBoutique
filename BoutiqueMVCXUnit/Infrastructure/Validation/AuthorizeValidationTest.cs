using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueMVC.Infrastructure.Implementation.Validation;
using BoutiqueMVC.Models.Implementations.Identity;
using Xunit;

namespace BoutiqueMVCXUnit.Infrastructure.Validation
{
    /// <summary>
    /// Проверка авторизации
    /// </summary>
    public class AuthorizeValidationTest
    {
        /// <summary>
        /// Проверить авторизацию
        /// </summary>
        [Fact]
        public void Validate_Ok()
        {
            var authorize = new AuthorizeDomain("rubilnik4@yandex.ru", "password1919");
            var validate = AuthorizeValidation.AuthorizeValidate(authorize, AuthorizeSettings);

            Assert.True(validate.OkStatus);
        }

        /// <summary>
        /// Проверить авторизацию
        /// </summary>
        [Fact]
        public void Validate_EmailError()
        {
            var authorize = new AuthorizeDomain("rubilnik4", "password1919");
            var validate = AuthorizeValidation.AuthorizeValidate(authorize, AuthorizeSettings);

            Assert.True(validate.HasErrors);
        }

        /// <summary>
        /// Проверить авторизацию
        /// </summary>
        [Fact]
        public void Validate_PasswordError()
        {
            var authorize = new AuthorizeDomain("rubilnik4@yandex.ru", "password");
            var validate = AuthorizeValidation.AuthorizeValidate(authorize, AuthorizeSettings);

            Assert.True(validate.HasErrors);
        }

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        private static AuthorizeSettings AuthorizeSettings =>
            new(8, true, false, true);
    }
}