using System.Linq;
using BoutiqueCommonXUnit.Data.Authorize;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Identities
{
    /// <summary>
    /// Авторизация. Тесты
    /// </summary>
    public class AuthorizeTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Authorize_Equal_Authorize()
        {
            var first = AuthorizeData.AuthorizeDomains.First();
            var second = AuthorizeData.AuthorizeDomains.Last();

            Assert.True(first.Equals(second));
        }
    }
}