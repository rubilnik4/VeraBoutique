using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data;
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
        public static List<ClothesTypeFullEntity> ClothesTypeEntities =>
            ClothesTypeData.GetClothesTypeFullDomain().
            Select(clothesTypeDomain => new ClothesTypeFullEntity(clothesTypeDomain.Name, 
                                                              new CategoryEntity(clothesTypeDomain.Category.Name))).
            ToList();

        /// <summary>
        /// Получить пол с видом одежды
        /// </summary>
        public static IList<ClothesTypeGenderCompositeEntity> GetClothesTypeGenderCompositeEntities(GenderEntity genderEntity,
                                                                                                    IReadOnlyCollection<ClothesTypeFullEntity> clothesTypeEntities) =>
            clothesTypeEntities.
            Select(clothesTypeEntity => new ClothesTypeGenderCompositeEntity(clothesTypeEntity.Id, genderEntity.Id,
                                                                    clothesTypeEntity, genderEntity)).
            ToList();

        /// <summary>
        /// Получить сущности типа одежды c информацией об одежде
        /// </summary>
        public static List<ClothesTypeFullEntity> GetClothesTypeEntitiesWithClothes(IReadOnlyCollection<ClothesTypeFullEntity> clothesTypeEntities,
                                                                                           IReadOnlyCollection<ClothesEntity> clothesInformationEntities) =>
            clothesTypeEntities.
            Select(clothesType => new ClothesTypeFullEntity(clothesType.Name, clothesType.CategoryName, clothesType.Category,
                                                        clothesInformationEntities,
                                                        Enumerable.Empty<ClothesTypeGenderCompositeEntity>())).
            ToList();
    }
}