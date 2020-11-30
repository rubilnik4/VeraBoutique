using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;

namespace BoutiqueDALXUnit.Data.Entities
{
    /// <summary>
    /// Данные сущностей вида одежды
    /// </summary>
    public static class ClothesTypeEntitiesData
    {
        /// <summary>
        /// Сущности вида одежды
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeEntity> ClothesTypeEntities =>
            ClothesTypeData.ClothesTypeDomain.
            Select(clothesTypeDomain => new ClothesTypeEntity(clothesTypeDomain, 
                                                              new CategoryEntity(clothesTypeDomain.Category.Name),
                                                              GetClothesTypeGenderCompositeEntities(clothesTypeDomain.Genders,
                                                                                                    clothesTypeDomain.Name))).
            ToList();

        /// <summary>
        /// Получить пол с видом одежды
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeGenderCompositeEntity> GetClothesTypeGenderCompositeEntities(GenderEntity genderEntity,
                                                                                                    IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities) =>
            clothesTypeEntities.
            Select(clothesTypeEntity => new ClothesTypeGenderCompositeEntity(clothesTypeEntity.Id, genderEntity.Id,
                                                                             clothesTypeEntity, genderEntity)).
            ToList();

        /// <summary>
        /// Получить сущности типа одежды c информацией об одежде
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeEntity> GetClothesTypeEntitiesWithClothes(IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities,
                                                                                IReadOnlyCollection<ClothesEntity> clothesEntities) =>
            clothesTypeEntities.
            Select(clothesType => new ClothesTypeEntity(clothesType.Name, clothesType.CategoryName, clothesType.Category,
                                                        Enumerable.Empty<ClothesTypeGenderCompositeEntity>(), clothesEntities)).
            ToList();

        /// <summary>
        /// Получить пол с видом одежды
        /// </summary>
        private static IEnumerable<ClothesTypeGenderCompositeEntity> GetClothesTypeGenderCompositeEntities(IEnumerable<IGenderDomain> genderDomains,
                                                                                                           string clothesTypeName) =>
            genderDomains.
            Select(genderDomain => new ClothesTypeGenderCompositeEntity(clothesTypeName, genderDomain.GenderType, 
                                                                        null, new GenderEntity(genderDomain.GenderType, genderDomain.Name)));
    }
}