using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommonXUnit.Data.Authorize;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Identity
{
    /// <summary>
    /// Регистрация. Тесты
    /// </summary>
    public class RegisterTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Authorize_Equal_Authorize()
        {
            var first = RegisterData.RegisterDomains.First();
            var second = RegisterData.RegisterDomains.Last();

            Assert.True(first.Equals(second));
        }
    }
}