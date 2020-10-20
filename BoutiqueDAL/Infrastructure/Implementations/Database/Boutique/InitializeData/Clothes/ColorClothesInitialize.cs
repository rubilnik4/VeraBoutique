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
        public static IReadOnlyCollection<ColorClothesEntity> ColorClothesData =>
            new List<ColorClothesEntity>
            {
                new ColorClothesEntity("Белый"),
                new ColorClothesEntity("Черный"),
            }.AsReadOnly();
    }
}