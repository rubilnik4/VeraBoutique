using System;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Implementation.Clothes;
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
        public void Equal_Ok()
        {
            const GenderType genderType = GenderType.Male;
            const string genderName = "Мужик";

            var gender = new Gender(genderType, genderName);

            int genderHash = HashCode.Combine(genderType, genderName);
            Assert.Equal(genderHash, gender.GetHashCode());
        }
    }
}