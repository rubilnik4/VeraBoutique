using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiqueCommonXUnit.Data.Clothes
{
    /// <summary>
    /// Данные цвета одежды
    /// </summary>
    public static class ColorClothesData
    {
        /// <summary>
        /// Получить категории одежды
        /// </summary>
        public static IReadOnlyCollection<IColorClothesDomain> ColorClothesDomain =>
            new List<IColorClothesDomain>()
            {
                new ColorClothesDomain("Черный"),
                new ColorClothesDomain("Дрисливый"),
            };
    }
}