using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiquePrerequisites.Factories.DatabaseInitialize.Boutique
{
    /// <summary>
    /// Начальные данные таблицы цвета одежды
    /// </summary>
    public class ColorInitialize
    {
        /// <summary>
        /// Начальные данные таблицы категорий одежды
        /// </summary>
        public static IReadOnlyCollection<IColorDomain> ColorClothes =>
            new List<ColorDomain>
            {
                new ("Белый"),
                new ("Черный"),
            }.AsReadOnly();
    }
}