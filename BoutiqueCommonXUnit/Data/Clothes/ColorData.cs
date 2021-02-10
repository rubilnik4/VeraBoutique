using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiqueCommonXUnit.Data.Clothes
{
    /// <summary>
    /// Данные цвета одежды
    /// </summary>
    public static class ColorData
    {
        /// <summary>
        /// Получить категории одежды
        /// </summary>
        public static IReadOnlyCollection<IColorDomain> ColorDomains =>
            new List<IColorDomain>()
            {
                new ColorDomain("Черный"),
                new ColorDomain("Дрисливый"),
            };
    }
}