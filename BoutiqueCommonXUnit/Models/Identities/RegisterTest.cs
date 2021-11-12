using System.Linq;
using BoutiqueCommonXUnit.Data.Authorize;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Identities
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
            var second = RegisterData.RegisterDomains.First();

            Assert.True(first.Equals(second));
        }
    }
}