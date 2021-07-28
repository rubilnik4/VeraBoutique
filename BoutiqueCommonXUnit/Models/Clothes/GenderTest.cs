using System;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
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

            var genderDomain = new GenderDomain(genderType, genderName);

            int genderHash = HashCode.Combine(genderType);
            Assert.Equal(genderHash, genderDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void GenderCategory_Equal_Ok()
        {
            const GenderType genderType = GenderType.Male;
            const string genderName = "Мужик";
            var categoryClothesTypeDomains = CategoryData.CategoryClothesTypeDomains;

            var genderCategoryDomain = new GenderCategoryDomain(genderType, genderName, categoryClothesTypeDomains);

            int genderCategoryHash = HashCode.Combine(genderType, categoryClothesTypeDomains.Average(categoryDomain => categoryDomain.GetHashCode()));
            Assert.Equal(genderCategoryHash, genderCategoryDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Gender_Equal_Gender()
        {
            var first = GenderData.GenderDomains.First();
            var second = new GenderDomain(first.GenderType, first.Name);

            Assert.True(first.Equals(second));
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void GenderCategory_Equal_GenderCategory()
        {
            var first = GenderData.GenderCategoryDomains.First();
            var second = new GenderCategoryDomain(first.GenderType, first.Name, first.Categories);

            Assert.True(first.Equals(second));
        }
    }
}