using System;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Clothes
{
    /// <summary>
    /// Одежда. Тесты
    /// </summary>
    public class ClothesShortTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ClothesShort_Equal_Ok()
        {
            const int id = 1;
            const string name = "Полушубок";
            const decimal price = 0.55m;
            var clothesShort = new ClothesShortDomain(id, name, price, null);

            int clothesHash = HashCode.Combine(id, name, price);
            Assert.Equal(clothesHash, clothesShort.GetHashCode());
        }
    }
}