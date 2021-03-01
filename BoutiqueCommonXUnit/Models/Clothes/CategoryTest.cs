using System;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommonXUnit.Data.Clothes;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Clothes
{
    /// <summary>
    /// Категория одежды. Тесты
    /// </summary>
    public class CategoryTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Category_Equal_Ok()
        {
            const string category = "обувь";

            var categoryDomain = new CategoryDomain(category);

            int categoryHash = HashCode.Combine(category);
            Assert.Equal(categoryHash, categoryDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Category_Equal_Category()
        {
            var first = CategoryData.CategoryDomains.First();
            var second = CategoryData.CategoryDomains.First();

            Assert.True(first.Equals(second));
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void CategoryMain_Equal_Ok()
        {
            const string category = "обувь";
            var genders = GenderData.GenderDomains;
            var categoryMainDomain = new CategoryMainDomain(category, genders);

            int categoryMainHash = HashCode.Combine(category, genders.Average(gender => gender.GetHashCode()));
            Assert.Equal(categoryMainHash, categoryMainDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void CategoryMain_Equal_CategoryMain()
        {
            var first = CategoryData.CategoryMainDomains.First();
            var second = CategoryData.CategoryMainDomains.First();

            Assert.True(first.Equals(second));
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void CategoryClothesType_Equal_Ok()
        {
            const string category = "обувь";
            var clothesTypes = ClothesTypeData.ClothesTypeDomains;
            var categoryClothesTypeDomain = new CategoryClothesTypeDomain(category, clothesTypes);

            int categoryClothesTypeHash = HashCode.Combine(category, clothesTypes.Average(clothesType => clothesType.GetHashCode()));
            Assert.Equal(categoryClothesTypeHash, categoryClothesTypeDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void CategoryClothesType_Equal_CategoryClothesType()
        {
            var first = CategoryData.CategoryClothesTypeDomains.First();
            var second = CategoryData.CategoryClothesTypeDomains.First();

            Assert.True(first.Equals(second));
        }
    }
}