using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
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
            var gender = new GenderDomain(GenderType.Male, "Мужик");
            var clothesType = new ClothesTypeDomain("Тряпье нательное", new CategoryDomain("Тряпки"));
            var colors = new List<IColorClothesDomain> { new ColorClothesDomain("Бежевый") };
            var sizes = new List<ISizeDomain> {new SizeDomain(SizeType.American, "1")};
            var sizeGroups = new List<ISizeGroupDomain> {new SizeGroupDomain(ClothesSizeType.Shirt , 1 , sizes) };
            var clothesShort = new ClothesInformationDomain(id, name, price, null, description, 
                                                            gender, clothesType, colors, sizeGroups);

            int clothesHash = HashCode.Combine(id, name, price, description, 
                                               clothesType.GetHashCode(), gender.GetHashCode(),
                                               colors.Average(color => color.GetHashCode()),
                                               sizeGroups.Average(size => size.GetHashCode()));
            Assert.Equal(clothesHash, clothesShort.GetHashCode());
        }
    }
}