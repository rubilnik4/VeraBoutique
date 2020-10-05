using System;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Clothes
{
    /// <summary>
    /// Размер одежды. Тесты
    /// </summary>
    public class ClothesSizeTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ClothesSize_Equal_Ok()
        {
            const ClothesSizeType clothesSizeType = ClothesSizeType.American;
            const int size = 72;
            const string sizeName = "72/74";

            var clothesSizeDomain = new ClothesSizeDomain(clothesSizeType, size, sizeName);

            int genderHash = HashCode.Combine(clothesSizeType, size);
            Assert.Equal(genderHash, clothesSizeDomain.GetHashCode());
        }

        /// <summary>
        /// Преобразование в строковое значение
        /// </summary>
        [Theory]
        [InlineData(ClothesSizeType.American, 'M', "M", "M")]
        [InlineData(ClothesSizeType.European, 73, "72/74", "EU 72/74")]
        [InlineData(ClothesSizeType.Russian, 156, "156/158", "RU 156/158")]
        public void ClothesSize_ToString(ClothesSizeType clothesSizeType, int size, string sizeName, string expectedString)
        {
            var clothesSize = new ClothesSizeDomain(clothesSizeType, size, sizeName);
            var clothesSizeString = clothesSize.ToString();

            Assert.Equal(expectedString, clothesSizeString);
        }
    }
}