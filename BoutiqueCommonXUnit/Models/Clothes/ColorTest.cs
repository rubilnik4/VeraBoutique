using System;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Clothes
{
    /// <summary>
    /// Цвет одежды. Тесты
    /// </summary>
    public class ColorTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ColorClothes_Equal_Ok()
        {
            const string name = "дрисливый";

            var colorClothesDomain = new ColorDomain(name);

            int colorClothesHash = HashCode.Combine(name);
            Assert.Equal(colorClothesHash, colorClothesDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Color_Equal_Color()
        {
            var first = ColorData.ColorDomains.First();
            var second = ColorData.ColorDomains.First();

            Assert.True(first.Equals(second));
        }

        /// <summary>
        /// Проверка перевода в строку
        /// </summary>
        [Fact]
        public void Color_ToString()
        {
            var color = ColorData.ColorDomains.First();
            string? colorString = color.ToString();

            Assert.Equal(colorString, color.Name);
        }
    }
}