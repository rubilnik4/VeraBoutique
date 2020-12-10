using System.Collections.Generic;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Clothes
{
    /// <summary>
    /// Начальные данные таблицы цвета одежды
    /// </summary>
    public class ColorClothesInitialize
    {
        /// <summary>
        /// Начальные данные таблицы категорий одежды
        /// </summary>
        public static IReadOnlyCollection<ColorEntity> ColorClothesData =>
            new List<ColorEntity>
            {
                new ColorEntity("Белый"),
                new ColorEntity("Черный"),
            }.AsReadOnly();
    }
}