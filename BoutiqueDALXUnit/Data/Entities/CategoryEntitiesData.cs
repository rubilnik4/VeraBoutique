using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;

namespace BoutiqueDALXUnit.Data.Entities
{
    /// <summary>
    /// Данные сущностей категорий одежды
    /// </summary>
    public static class CategoryEntitiesData
    {
        /// <summary>
        /// Сущности категорий одежды
        /// </summary>
        public static List<CategoryEntity> CategoryEntities =>
            CategoryData.GetCategoryDomain().
                         Select(categoryDomain => new CategoryEntity(categoryDomain.Name)).
                         ToList();

        /// <summary>
        /// Получить сущности категорий c видом одежды
        /// </summary>
        public static List<CategoryEntity> GetCategoryEntitiesWithClothesType(IReadOnlyCollection<CategoryEntity> categoryEntities,
                                                                              IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities) =>
            categoryEntities.
                Select(category => new CategoryEntity(category.Name, clothesTypeEntities)).
                ToList();
    }
}