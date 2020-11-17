using System.Collections.Generic;
using System.Linq;
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
        public static List<ClothesTypeEntity> ClothesTypeEntities =>
            ClothesTypeData.ClothesTypeDomain.
            Select(clothesTypeDomain => new ClothesTypeEntity(clothesTypeDomain, 
                                                              new CategoryEntity(clothesTypeDomain.Category.Name),
                                                              Enumerable.Empty<ClothesEntity>(),
                                                              Enumerable.Empty<ClothesTypeGenderCompositeEntity>())).
            ToList();

        /// <summary>
        /// Получить пол с видом одежды
        /// </summary>
        public static IList<ClothesTypeGenderCompositeEntity> GetClothesTypeGenderCompositeEntities(GenderEntity genderEntity,
                                                                                                    IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities) =>
            clothesTypeEntities.
            Select(clothesTypeEntity => new ClothesTypeGenderCompositeEntity(clothesTypeEntity.Id, genderEntity.Id,
                                                                    clothesTypeEntity, genderEntity)).
            ToList();

        /// <summary>
        /// Получить сущности типа одежды c информацией об одежде
        /// </summary>
        public static List<ClothesTypeEntity> GetClothesTypeEntitiesWithClothes(IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities,
                                                                                           IReadOnlyCollection<ClothesEntity> clothesInformationEntities) =>
            clothesTypeEntities.
            Select(clothesType => new ClothesTypeEntity(clothesType.Name, clothesType.CategoryName, clothesType.Category,
                                                        clothesInformationEntities,
                                                        Enumerable.Empty<ClothesTypeGenderCompositeEntity>())).
            ToList();
    }
}