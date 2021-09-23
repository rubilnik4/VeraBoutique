using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommonXUnit.Data.Authorize;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Identity
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
            var second = PersonalData.PersonalDomains.Last();

            Assert.True(first.Equals(second));
        }
    }
}