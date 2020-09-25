using System;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Clothes
{
    /// <summary>
    /// Пол для одежды. Тесты
    /// </summary>
    public class GenderTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Gender_Equal_Ok()
        {
            const GenderType genderType = GenderType.Male;
            const string genderName = "Мужик";

            var gender = new GenderDomain(genderType, genderName);

            int genderHash = HashCode.Combine(genderType);
            Assert.Equal(genderHash, gender.GetHashCode());
        }
    }
}