﻿using System;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation;
using BoutiqueCommon.Infrastructure.Implementation.Clothes;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
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
            const SizeType sizeType = SizeType.American;
            const string sizeName = "72/74";

            var clothesSizeDomain = new SizeDomain(sizeType, sizeName);

            int genderHash = HashCode.Combine(sizeType, sizeName);
            Assert.Equal(genderHash, clothesSizeDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Size_Equal_Size()
        {
            var first = SizeData.SizeDomains.First();
            var second = new SizeDomain(first.SizeType, first.Name);

            Assert.True(first.Equals(second));
        }

        /// <summary>
        /// Преобразование в строковое значение
        /// </summary>
        [Theory]
        [InlineData(SizeType.American, "M", "M")]
        [InlineData(SizeType.European, "72/74", "EU 72/74")]
        [InlineData(SizeType.Russian, "156/158", "RU 156/158")]
        public void ClothesSize_ToString(SizeType clothesSizeType, string sizeName, string expectedString)
        {
            string clothesSizeShortName = SizeNaming.GetSizeNameShort(clothesSizeType, sizeName);

            Assert.Equal(expectedString, clothesSizeShortName);
        }
    }
}