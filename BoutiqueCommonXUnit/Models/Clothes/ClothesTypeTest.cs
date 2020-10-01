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
            const string clothesType = "свитер";

            var clothesTypeDomain = new ClothesTypeDomain(clothesType);

            int clothesTypeHash = HashCode.Combine(clothesType);
            Assert.Equal(clothesTypeHash, clothesTypeDomain.GetHashCode());
        }
    }
}