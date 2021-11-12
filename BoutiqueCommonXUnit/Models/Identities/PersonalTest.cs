using System.Linq;
using BoutiqueCommonXUnit.Data.Authorize;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Identities
{
    /// <summary>
    /// Личная информация. Тесты
    /// </summary>
    public class PersonalTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Authorize_Equal_Authorize()
        {
            var first = PersonalData.PersonalDomains.First();
            var second = PersonalData.PersonalDomains.First();

            Assert.True(first.Equals(second));
        }
    }
}