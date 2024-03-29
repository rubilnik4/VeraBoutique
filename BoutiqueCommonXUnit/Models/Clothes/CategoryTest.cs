﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
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
            var second = new CategoryDomain(first.Name);

            Assert.True(first.Equals(second));
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void CategoryMain_Equal_CategoryMain()
        {
            var first = CategoryData.CategoryMainDomains.First();
            var second = new CategoryMainDomain(first, first.Genders);

            Assert.True(first.Equals(second));
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void CategoryMain_Equal_GenderCategory()
        {
            var first = CategoryData.CategoryMainDomains.First();
            var second = new CategoryMainDomain(first, GenderData.GenderCategoryDomains);

            Assert.True(first.Equals(second));
            Assert.True(second.Equals(first));
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void CategoryMain_Equal_ClothesTypeMain()
        {
            var first = CategoryData.CategoryClothesTypeDomains.First();
            var second = new CategoryClothesTypeDomain(first, ClothesTypeData.ClothesTypeMainDomains);

            Assert.True(first.Equals(second));
            Assert.True(second.Equals(first));
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