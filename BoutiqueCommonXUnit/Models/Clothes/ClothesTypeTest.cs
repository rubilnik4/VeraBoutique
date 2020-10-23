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
            var clothesTypeDomain = new ClothesTypeDomain(clothesType, categoryDomain);

            int clothesTypeHash = HashCode.Combine(clothesType, categoryDomain.GetHashCode());
            Assert.Equal(clothesTypeHash, clothesTypeDomain.GetHashCode());
        }
    }
}