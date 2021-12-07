using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Extensions.HashCodeExtensions;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.Images;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueCommonXUnit.Properties;
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
        public void Clothes_Equal_Ok()
        {
            const int id = 1;
            const string name = "Полушубок";
            const string description = "Полушубок дерзкий";
            const decimal price = 0.55m;
            const GenderType genderType = GenderType.Child;
            const string clothesTypeName = "Одежа";
            var clothes = new ClothesDomain(id, name, description, price, genderType, clothesTypeName);

            int clothesHash = HashCode.Combine(id);
            Assert.Equal(clothesHash, clothes.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Clothes_Equal_Clothes()
        {
            var first = ClothesData.ClothesDomains.First();
            var second = new ClothesDomain(first.Id, first.Name, first.Description, first.Price,
                                           first.GenderType, first.ClothesTypeName);

            Assert.True(first.Equals(second));
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ClothesDetail_Equal_ClothesDetail()
        {
            var first = ClothesData.ClothesDetailDomains.First();
            var second = new ClothesDetailDomain(first.Id, first.Name, first.Description, first.Price,
                                                 first.GenderType, first.ClothesTypeName, first.Colors, first.SizeGroups);

            Assert.True(first.Equals(second));
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ClothesMain_Equal_ClothesMain()
        {
            var first = ClothesData.ClothesMainDomains.First();
            var second = new ClothesMainDomain(first.Id, first.Name, first.Description, first.Price, first.Images,
                                               first.Gender, first.ClothesType, first.Colors, first.SizeGroups);

            Assert.True(first.Equals(second));
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ClothesDetail_Equal_Color()
        {
            var first = ClothesData.ClothesDomains.First();
            var second = new ClothesDetailDomain(first, ColorData.ColorDomains, SizeGroupData.SizeGroupMainDomains);

            Assert.True(first.Equals(second));
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ClothesMain_Equal_Color()
        {
            var first = ClothesData.ClothesDomains.First();
            var images = new List<IClothesImageDomain> { new ClothesImageDomain(Guid.NewGuid(), Resources.TestImage, true, 0) };
            var second = new ClothesMainDomain(first, images, GenderData.GenderCategoryDomains.First(),
                                              ClothesTypeData.ClothesTypeMainDomains.First(),
                                              ColorData.ColorDomains, SizeGroupData.SizeGroupMainDomains);

            Assert.True(first.Equals(second));
        }
    }
}