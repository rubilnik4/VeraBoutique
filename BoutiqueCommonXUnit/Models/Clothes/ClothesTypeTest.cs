﻿using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
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
        public void ClothesType_Equal_Ok()
        {
            const string name = "Свитер";
            const string categoryName = "Нательное";
            var clothesTypeDomain = new ClothesTypeDomain(name, categoryName);

            int clothesTypeHash = HashCode.Combine(name, categoryName);
            Assert.Equal(clothesTypeHash, clothesTypeDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности поной информации вида одежды
        /// </summary>
        [Fact]
        public void ClothesTypeMain_Equal_Ok()
        {
            const string name = "Свитер";
            var categoryDomain = new CategoryDomain("Верхушка");
            var clothesTypeMainDomain = new ClothesTypeMainDomain(name, categoryDomain);

            int clothesTypeHash = HashCode.Combine(name, categoryDomain.GetHashCode());
            Assert.Equal(clothesTypeHash, clothesTypeMainDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ClothesType_Equal_ClothesType()
        {
            var first = ClothesTypeData.ClothesTypeMainDomains.First();
            var second = ClothesTypeData.ClothesTypeMainDomains.First();

            Assert.True(first.Equals(second));
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ClothesTypeShort_Equal_ClothesTypeShort()
        {
            var first = ClothesTypeData.ClothesTypeDomains.First();
            var second = ClothesTypeData.ClothesTypeDomains.First();

            Assert.True(first.Equals(second));
        }
    }
}