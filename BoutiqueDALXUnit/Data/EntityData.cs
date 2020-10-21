using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDALXUnit.Data
{
    /// <summary>
    /// Тестовые данные сущностей
    /// </summary>
    public static class EntityData
    {
        /// <summary>
        /// Сущности типа пола
        /// </summary>
        public static List<GenderEntity> GenderEntities =>
            GenderData.GetGendersDomain().
            Select(genderDomain => new GenderEntity(genderDomain.GenderType, genderDomain.Name)).
            ToList();

        /// <summary>
        /// Сущности вида одежды
        /// </summary>
        public static List<ClothesTypeEntity> ClothesTypeEntities =>
            ClothesTypeData.GetClothesTypeDomain().
            Select(clothesTypeDomain => new ClothesTypeEntity(clothesTypeDomain.Name)).
            ToList();

        /// <summary>
        /// Сущности категорий одежды
        /// </summary>
        public static List<CategoryEntity> CategoryEntities =>
            CategoryData.GetCategoryDomain().
            Select(categoryDomain => new CategoryEntity(categoryDomain.Name)).
            ToList();

        /// <summary>
        /// Сущности группы размеров
        /// </summary>
        public static List<SizeGroupEntity> SizeGroupEntities =>
            SizeGroupData.GetSizeGroupDomain().
            Select(sizeGroup => new SizeGroupEntity(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize,
                                                    GetSizeGroupComposite(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize,
                                                                          sizeGroup.Sizes))).
            ToList();
            

        /// <summary>
        /// Получить сущности типа пола c видом одежды
        /// </summary>
        public static List<GenderEntity> GetGenderEntitiesWithClothesType(IReadOnlyCollection<GenderEntity> genderEntities,
                                                                          IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities) =>
            genderEntities.
            Select(gender => new GenderEntity(gender.GenderType, gender.Name,
                                              GetClothesTypeGenderEntity(gender, clothesTypeEntities))).
            ToList();

        /// <summary>
        /// Получить сущности категорий c видом одежды
        /// </summary>
        public static List<CategoryEntity> GetCategoryEntitiesWithClothesType(IReadOnlyCollection<CategoryEntity> categoryEntities,
                                                                              IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities) =>
            categoryEntities.
            Select(category => new CategoryEntity(category.Name, clothesTypeEntities)).
            ToList();

        /// <summary>
        /// Получить пол с видом одежды
        /// </summary>
        public static IList<ClothesTypeGenderCompositeEntity> GetClothesTypeGenderEntity(GenderEntity genderEntity,
                                                                                IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities) =>
            clothesTypeEntities.
            Select(clothesTypeEntity => new ClothesTypeGenderCompositeEntity(clothesTypeEntity.Id, genderEntity.Id, 
                                                                    clothesTypeEntity, genderEntity)).
            ToList();

        /// <summary>
        /// Получить сущности для теста
        /// </summary>
        public static List<TestEntity> TestEntities =>
            TestData.GetTestDomains().
            Select(testDomain => new TestEntity(testDomain.TestEnum, testDomain.Name)).
            ToList();

        /// <summary>
        /// Тестовые сущности в результирующей коллекции
        /// </summary>
        public static IResultCollection<TestEntity> TestResultEntities =>
            new ResultCollection<TestEntity>(TestEntities);

        /// <summary>
        /// Пустая коллекция результирующих сущностей
        /// </summary>
        public static IResultCollection<TestEntity> TestResultEntitiesEmpty =>
           new ResultCollection<TestEntity>(Enumerable.Empty<TestEntity>());

        /// <summary>
        /// Получить связующую сущность группы размеров
        /// </summary>
        private static IEnumerable<SizeGroupCompositeEntity> GetSizeGroupComposite(ClothesSizeType clothesSizeType, int sizeNormalize,
                                                                                   IEnumerable<ISizeDomain> sizes) =>
            sizes.Select(size => new SizeGroupCompositeEntity(size.SizeType, size.SizeName, clothesSizeType, sizeNormalize, 
                                                              new SizeEntity(size.SizeType, size.SizeName),
                                                              new SizeGroupEntity(clothesSizeType, sizeNormalize)));
    }
}