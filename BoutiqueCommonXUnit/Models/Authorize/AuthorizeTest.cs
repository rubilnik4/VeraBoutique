using System;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueCommonXUnit.Data.Clothes;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Authorize
{
    /// <summary>
    /// Логин и пароль. Тесты
    /// </summary>
    public class AuthorizeTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Authorize_Equal_Ok()
        {
            const string login = "login";
            const string password = "password";

            var categoryDomain = new AuthorizeDomain(login, password);

            int categoryHash = HashCode.Combine(login, password);
            Assert.Equal(categoryHash, categoryDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Authorize_Equal_Authorize()
        {
            var first = AuthorizeData.AuthorizeDomain.First();
            var second = AuthorizeData.AuthorizeDomain.First();

            Assert.True(first.Equals(second));
        }
    }
}