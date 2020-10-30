using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Clothes
{
    /// <summary>
    /// Вид одежды. Тесты
    /// </summary>
    public class ClothesTypeTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ClothesType_Equal_Ok()
        {
            const string clothesType = "Свитер";
            var categoryDomain = new CategoryDomain("Верхушка");
            var genderDomain = new GenderDomain(GenderType.Male, "Мужичок");
            var clothesTypeDomain = new ClothesTypeDomain(clothesType, genderDomain, categoryDomain);

            int clothesTypeHash = HashCode.Combine(clothesType, clothesTypeDomain.GetHashCode(), categoryDomain.GetHashCode());
            Assert.Equal(clothesTypeHash, clothesTypeDomain.GetHashCode());
        }
    }
}