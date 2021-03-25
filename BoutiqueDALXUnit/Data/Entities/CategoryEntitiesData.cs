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
using Xunit.Sdk;

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
        /// Получить категории одежды с типом пола
        /// </summary>
        public static IEnumerable<CategoryEntity> GetCategoryEntitiesWithGenders(IEnumerable<CategoryEntity> categories,
                                                                                 IEnumerable<GenderEntity> genders) =>
            categories.
            Select(category => new CategoryEntity(category.Name,
                                                  genders.Select(gender => new GenderCategoryCompositeEntity(gender.GenderType, category.Name,
                                                                                                             gender, category))));

        /// <summary>
        /// Получить категории одежды с типом одежды
        /// </summary>
        public static IEnumerable<CategoryEntity> GetCategoryEntitiesWithClothesTypes(IEnumerable<CategoryEntity> categories,
                                                                                      IEnumerable<ClothesTypeEntity> clothesTypes) =>
            categories.
            Select(category =>
                new CategoryEntity(category.Name, clothesTypes));

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
        /// Получить связующую сущность группы размеров
        /// </summary>
        public static IEnumerable<GenderCategoryCompositeEntity> GetCategoryComposite(GenderType genderType,
                                                                                      IEnumerable<ICategoryClothesTypeDomain> categories) =>
            categories.Select(category => new GenderCategoryCompositeEntity(genderType, category.Name, null,
                                                                            GetCategoryClothesTypeEntity(category)));
        /// <summary>
        /// Получить категорию с типом одежды
        /// </summary>
        public static CategoryEntity GetCategoryClothesTypeEntity(ICategoryClothesTypeDomain category) =>
            new(category.Name, null,
                category.ClothesTypes.Select(clothesType => new ClothesTypeEntity(clothesType)));
    }
}