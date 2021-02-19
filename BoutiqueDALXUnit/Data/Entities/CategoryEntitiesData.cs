using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;

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
        public static IReadOnlyCollection<CategoryEntity> CategoryEntities =>
            CategoryData.CategoryMainDomains.
                         Select(GetCategoryEntity).
                         ToList();

        /// <summary>
        /// Получить сущность группы размеров
        /// </summary>
        public static CategoryEntity GetCategoryEntity(ICategoryMainDomain category) =>
             new(category, GetCategoryComposite(category.Name, category.Genders));

        /// <summary>
        /// Получить связующую сущность группы размеров
        /// </summary>
        public static IEnumerable<GenderCategoryCompositeEntity> GetCategoryComposite(string categoryName, IEnumerable<IGenderDomain> genders) =>
            genders.Select(gender => new GenderCategoryCompositeEntity(gender.GenderType, categoryName, new GenderEntity(gender), null));

        /// <summary>
        /// Получить сущности категорий c видом одежды
        /// </summary>
        public static IReadOnlyCollection<CategoryEntity> GetCategoryClothesTypeEntities(IReadOnlyCollection<CategoryEntity> categoryEntities,
                                                                                         IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities) =>
            categoryEntities.
            Select(category => new CategoryEntity(category.Name, category.GenderCategoryComposites, clothesTypeEntities)).
            ToList();
    }
}