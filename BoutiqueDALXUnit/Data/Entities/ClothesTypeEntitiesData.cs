using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
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
            ClothesTypeData.GetClothesTypeDomain().
            Select(clothesTypeDomain => new ClothesTypeEntity(clothesTypeDomain.Name, 
                                                              new CategoryEntity(clothesTypeDomain.CategoryDomain.Name))).
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
    }
}