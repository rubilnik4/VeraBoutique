using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Clothes
{
    /// <summary>
    /// Вид одежды. Тесты
    /// </summary>
    public class ClothesTypeTest
    {
        /// <summary>
        /// Проверка идентичности поной информации вида одежды
        /// </summary>
        [Fact]
        public void ClothesTypeShort_Equal_Ok()
        {
            const string clothesType = "Свитер";
            const string categoryName = "Нательное";
            var clothesTypeDomain = new ClothesTypeShortDomain(clothesType, categoryName);

            int clothesTypeHash = HashCode.Combine(clothesType, categoryName);
            Assert.Equal(clothesTypeHash, clothesTypeDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности поной информации вида одежды
        /// </summary>
        [Fact]
        public void ClothesType_Equal_Ok()
        {
            const string clothesType = "Свитер";
            var categoryDomain = new CategoryDomain("Верхушка");
            var genderDomains = new List<IGenderDomain> { new GenderDomain(GenderType.Male, "Мужичок") };
            var clothesTypeShortDomain = new ClothesTypeDomain(clothesType, categoryDomain, genderDomains);

            int clothesTypeHash = HashCode.Combine(clothesType, categoryDomain.GetHashCode(),
                                                   genderDomains.Average(gender => gender.GetHashCode()));
            Assert.Equal(clothesTypeHash, clothesTypeShortDomain.GetHashCode());
        }
    }
}