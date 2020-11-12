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
            var categoryDomain = new CategoryDomain("Верхушка");
            var clothesTypeDomain = new ClothesTypeShortDomain(clothesType, categoryDomain);

            int clothesTypeHash = HashCode.Combine(clothesType, categoryDomain.GetHashCode());
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

        /// <summary>
        /// Проверка преобразования в полную версию
        /// </summary>
        [Fact]
        public void ToClothesTypeDomain_IEnumerable_Ok()
        {
            const string clothesType = "Свитер";
            var categoryDomain = new CategoryDomain("Верхушка");
            var genderDomains = new List<IGenderDomain> { new GenderDomain(GenderType.Male, "Мужичок") };
            var clothesTypeShortDomain = new ClothesTypeShortDomain(clothesType, categoryDomain);
            var clothesTypeDomain = new ClothesTypeDomain(clothesType, categoryDomain, genderDomains);

            var clothesTypeDomainConvert = clothesTypeShortDomain.ToClothesTypeDomain(genderDomains);

            Assert.True(clothesTypeDomainConvert.Equals(clothesTypeDomain));
        }

        /// <summary>
        /// Проверка преобразования в полную версию
        /// </summary>
        [Fact]
        public void ToClothesTypeDomain_Ok()
        {
            const string clothesType = "Свитер";
            var categoryDomain = new CategoryDomain("Верхушка");
            var genderDomain = new GenderDomain(GenderType.Male, "Мужичок") ;
            var clothesTypeShortDomain = new ClothesTypeShortDomain(clothesType, categoryDomain);
            var clothesTypeDomain = new ClothesTypeDomain(clothesType, categoryDomain,
                                                           new List<IGenderDomain> { genderDomain });

            var clothesTypeDomainConvert = clothesTypeShortDomain.ToClothesTypeDomain(genderDomain);

            Assert.True(clothesTypeDomainConvert.Equals(clothesTypeDomain));
        }
    }
}