using System;
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
            const int id = 1;
            var image = new byte[0];
            const bool isMain = false;

            var clothesImageDomain = new ClothesImageDomain(id, image, isMain);

            int clothesImageHash = HashCode.Combine(id);
            Assert.Equal(clothesImageHash, clothesImageDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ClothesImage_Equal_ClothesImage()
        {
            var first = ImageData.ClothesImageDomains.First();
            var second = new ClothesImageDomain(first.Id, first.Image, first.IsMain);

            Assert.True(first.Equals(second));
        }
    }
}