using BoutiqueCommon.Infrastructure.Implementation.Validation.Identities;
using BoutiqueCommon.Models.Common.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommonXUnit.Data.Authorize;
using Xunit;

namespace BoutiqueCommonXUnit.Infrastructure.Validation.Identities
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
            var validate = AuthorizeValidation.AuthorizeValidate(authorize, AuthorizeSettings.PasswordSettings);

            Assert.True(validate.OkStatus);
        }

        /// <summary>
        /// Проверить авторизацию
        /// </summary>
        [Fact]
        public void Validate_EmailError()
        {
            var authorize = new AuthorizeDomain("rubilnik4", "password1919");
            var validate = AuthorizeValidation.AuthorizeValidate(authorize, AuthorizeSettings.PasswordSettings);

            Assert.True(validate.HasErrors);
        }

        /// <summary>
        /// Проверить авторизацию
        /// </summary>
        [Fact]
        public void Validate_PasswordError()
        {
            var authorize = new AuthorizeDomain("rubilnik4@yandex.ru", "password");
            var validate = AuthorizeValidation.AuthorizeValidate(authorize, AuthorizeSettings.PasswordSettings);

            Assert.True(validate.HasErrors);
        }
    }
}