using System.Collections.Generic;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Clothes
{
    /// <summary>
    /// Начальные данные таблицы категорий одежды
    /// </summary>
    public class CategoryInitialize
    {
        /// <summary>
        /// ачальные данные таблицы категорий одежды
        /// </summary>
        public static IReadOnlyCollection<CategoryEntity> CategoryData =>
            new List<CategoryEntity>
            {
                new CategoryEntity("Верхняя одежда"),
                new CategoryEntity("Штаны"),
                new CategoryEntity("Обувь"),
                new CategoryEntity("Аксессуары"),
            }.AsReadOnly();
    }
}