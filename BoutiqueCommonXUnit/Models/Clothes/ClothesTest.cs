using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Clothes
{
    /// <summary>
    /// Одежда. Информация. Тесты
    /// </summary>
    public class ClothesTest
    {
        /// <summary>
        /// Проверка идентичности одежды
        /// </summary>
        [Fact]
        public void ClothesShort_Equal_Ok()
        {
            const int id = 1;
            const string name = "Полушубок";
            const string description = "Полушубок дерзкий";
            const decimal price = 0.55m;
            var clothesShort = new ClothesShortDomain(id, name, description, price, null);

            int clothesHash = HashCode.Combine(id, name, price);
            Assert.Equal(clothesHash, clothesShort.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности полной информации об одежде
        /// </summary>
        [Fact]
        public void Clothes_Equal_Ok()
        {
            const int id = 1;
            const string name = "Полушубок";
            const string description = "Полушубок красивый";
            const decimal price = (decimal)0.55;
            var gender = new GenderDomain(GenderType.Male, "Мужик");
            var clothesType = new ClothesTypeShortDomain("Тряпье нательное", "Тряпье");
            var colors = new List<IColorClothesDomain> { new ColorClothesDomain("Бежевый") };
            var sizes = new List<ISizeDomain> {new SizeDomain(SizeType.American, "1")};
            var sizeGroups = new List<ISizeGroupDomain> {new SizeGroupDomain(ClothesSizeType.Shirt , 1 , sizes) };
            var clothesShort = new ClothesDomain(id, name, description, price, null, gender, clothesType, colors, sizeGroups);

            int clothesHash = HashCode.Combine(id, name, price, description,
                                               gender.GetHashCode(), clothesType.GetHashCode(),
                                               colors.Average(color => color.GetHashCode()),
                                               sizeGroups.Average(size => size.GetHashCode()));
            Assert.Equal(clothesHash, clothesShort.GetHashCode());
        }
    }
}