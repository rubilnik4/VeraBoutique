using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
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
            const GenderType genderType = GenderType.Child;
            const string categoryName = "Одежа";
            byte[] image = Properties.Resources.TestImage;
            var clothesShort = new ClothesDomain(id, name, description, price, image, genderType, categoryName);

            int clothesHash = HashCode.Combine(id, name, description, price, image, genderType, categoryName);
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
            byte[] image = Properties.Resources.TestImage;
            var gender = new GenderDomain(GenderType.Male, "Мужик");
            var clothesType = new ClothesTypeDomain("Тряпье нательное", "Тряпье");
            var colors = new List<IColorDomain> { new ColorDomain("Бежевый") };
            var sizes = new List<ISizeDomain> { new SizeDomain(SizeType.American, "1") };
            var sizeGroups = new List<ISizeGroupMainDomain> { new SizeGroupMainDomain(ClothesSizeType.Shirt, 1, sizes) };
            var clothes = new ClothesMainDomain(id, name, description, price, image, gender, clothesType, colors, sizeGroups);

            int clothesHash = HashCode.Combine(HashCode.Combine(id, name, price, description, image),
                                               gender.GetHashCode(), clothesType.GetHashCode(),
                                               colors.Average(color => color.GetHashCode()),
                                               sizeGroups.Average(size => size.GetHashCode()));
            Assert.Equal(clothesHash, clothes.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ClothesMain_Equal_ClothesMain()
        {
            var first = ClothesData.ClothesDomains.First();
            var second = ClothesData.ClothesDomains.First();

            Assert.True(first.Equals(second));
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ClothesMain_Equal_Color()
        {
            var first = ClothesData.ClothesDomains.First();
            var second =new ClothesMainDomain(first, GenderData.GenderCategoryDomains.First(), 
                                              ClothesTypeData.ClothesTypeMainDomains.First(),
                                              ColorData.ColorDomains, SizeGroupData.SizeGroupMainDomains);

            Assert.True(first.Equals(second));
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Clothes_Equal_Clothes()
        {
            var first = ClothesData.ClothesDomains.First();
            var second = ClothesData.ClothesDomains.First();

            Assert.True(first.Equals(second));
        }
    }
}