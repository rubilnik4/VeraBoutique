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
            var genderDomain =  new GenderDomain(GenderType.Male, "Мужичок");
            var clothesTypeDomain = new ClothesTypeShortDomain(clothesType, categoryDomain, genderDomain);

            int clothesTypeHash = HashCode.Combine(clothesType, clothesTypeDomain.GetHashCode(), 
                                                   categoryDomain.GetHashCode(), genderDomain.GetHashCode());
            Assert.Equal(clothesTypeHash, clothesTypeDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности поной информации вида одежды
        /// </summary>
        [Fact]
        public void ClothesTypeFull_Equal_Ok()
        {
            const string clothesType = "Свитер";
            var categoryDomain = new CategoryDomain("Верхушка");
            var genderDomains = new List<IGenderDomain> { new GenderDomain(GenderType.Male, "Мужичок") };
            var clothesTypeDomain = new ClothesTypeFullDomain(clothesType, categoryDomain, genderDomains);

            int clothesTypeHash = HashCode.Combine(clothesType, clothesTypeDomain.GetHashCode(), categoryDomain.GetHashCode(),
                                                   genderDomains.Average(gender => gender.GetHashCode()));
            Assert.Equal(clothesTypeHash, clothesTypeDomain.GetHashCode());
        }

        /// <summary>
        /// Преобразовать в базовую модель
        /// </summary>
        [Fact]
        public void ToClothesTypeShort_Ok()
        {
            const string clothesType = "Свитер";
            var categoryDomain = new CategoryDomain("Верхушка");
            var genderDomains = new List<IGenderDomain> { new GenderDomain(GenderType.Male, "Мужичок") };
            var clothesTypeDomain = new ClothesTypeFullDomain(clothesType, categoryDomain, genderDomains);

            var clothesTypeShort = clothesTypeDomain.ToClothesTypeShort();

            Assert.True(clothesTypeShort.OkStatus);
            Assert.True(clothesTypeShort.Value.Gender.Equals(genderDomains.First()));
        }

        /// <summary>
        /// Преобразовать в базовую модель с ошибкой
        /// </summary>
        [Fact]
        public void ToClothesTypeShort_Error()
        {
            const string clothesType = "Свитер";
            var categoryDomain = new CategoryDomain("Верхушка");
            var genderDomains = Enumerable.Empty<IGenderDomain>();
            var clothesTypeDomain = new ClothesTypeFullDomain(clothesType, categoryDomain, genderDomains);

            var clothesTypeShort = clothesTypeDomain.ToClothesTypeShort();

            Assert.True(clothesTypeShort.HasErrors);
            Assert.True(clothesTypeShort.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }
    }
}