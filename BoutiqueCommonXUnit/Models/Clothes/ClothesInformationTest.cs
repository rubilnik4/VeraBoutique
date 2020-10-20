using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Clothes
{
    /// <summary>
    /// Одежда. Информация. Тесты
    /// </summary>
    public class ClothesInformationTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ClothesInformation_Equal_Ok()
        {
            const int id = 1;
            const string name = "Полушубок";
            const string description = "Полушубок красивый";
            const decimal price = (decimal)0.55;
            var colors = new List<string> { "Бежевый" };
            var sizes = new List<int> { 1, 2, 3 };
            var clothesShort = new ClothesInformationDomain(id, name, description, colors, sizes, price, null);

            int clothesHash = HashCode.Combine(id, name, price, description,
                                               colors.Average(color => color.GetHashCode()), 
                                               sizes.Average(size => size.GetHashCode()));
            Assert.Equal(clothesHash, clothesShort.GetHashCode());
        }
    }
}