﻿using System;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Clothes
{
    /// <summary>
    /// Размер одежды. Тесты
    /// </summary>
    public class SizeTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Size_Equal_Ok()
        {
            const SizeType clothesSizeType = SizeType.American;
            const int size = 72;
            const string sizeName = "72/74";

            var clothesSizeDomain = new SizeDomain(clothesSizeType, size, sizeName);

            int genderHash = HashCode.Combine(clothesSizeType, size);
            Assert.Equal(genderHash, clothesSizeDomain.GetHashCode());
        }

        /// <summary>
        /// Преобразование в строковое значение
        /// </summary>
        [Theory]
        [InlineData(SizeType.American, 'M', "M", "M")]
        [InlineData(SizeType.European, 73, "72/74", "EU 72/74")]
        [InlineData(SizeType.Russian, 156, "156/158", "RU 156/158")]
        public void ClothesSize_ToString(SizeType clothesSizeType, int size, string sizeName, string expectedString)
        {
            string clothesSizeShortName = Size.GetClothesSizeNameShort(clothesSizeType, sizeName);

            Assert.Equal(expectedString, clothesSizeShortName);
        }
    }
}