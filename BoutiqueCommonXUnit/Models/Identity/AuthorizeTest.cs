using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueCommonXUnit.Data.Clothes;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Identity
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