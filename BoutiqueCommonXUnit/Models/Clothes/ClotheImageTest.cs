﻿using System;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.Images;
using BoutiqueCommonXUnit.Data.Clothes;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Clothes
{
    public class ClotheImageTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ClothesImage_Equal_Ok()
        {
            var id = Guid.NewGuid();
            var image = Array.Empty<byte>();
            const bool isMain = false;

            var clothesImageDomain = new ClothesImageDomain(id, image, isMain, 0);

            int clothesImageHash = HashCode.Combine(id);
            Assert.Equal(clothesImageHash, clothesImageDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ClothesImage_Equal_ClothesImage()
        {
            var first = ClothesImageData.ClothesImageDomains.First();
            var second = new ClothesImageDomain(first.Id, first.Image, first.IsMain, 0);

            Assert.True(first.Equals(second));
        }
    }
}