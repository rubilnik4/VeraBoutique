using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDALXUnit.Data.Entities
{
    /// <summary>
    /// Данные сущностей типа пола
    /// </summary>
    public static class GenderEntitiesData
    {
        /// <summary>
        /// Сущности типа пола
        /// </summary>
        public static IReadOnlyCollection<GenderEntity> GenderEntities =>
            GenderData.GenderDomains.
            Select(genderDomain => new GenderEntity(genderDomain)).
            ToList();

        /// <summary>
        /// Получить сущности типа пола c видом одежды
        /// </summary>
        public static IReadOnlyCollection<GenderEntity> GetGenderEntitiesWithClothesType(IReadOnlyCollection<GenderEntity> genderEntities,
                                                                          IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities) =>
            genderEntities.
            Select(gender => new GenderEntity(gender.GenderType, gender.Name,
                                              ClothesTypeEntitiesData.GetClothesTypeGenderCompositeEntities(gender, clothesTypeEntities),
                                              null)).
            ToList();

        /// <summary>
        /// Получить сущности типа пола c информацией об одежде
        /// </summary>
        public static IReadOnlyCollection<GenderEntity> GetGenderEntitiesWithClothes(IReadOnlyCollection<GenderEntity> genderEntities,
                                                                                 IReadOnlyCollection<ClothesEntity> clothesInformationEntities) =>
            genderEntities.
            Select(gender => new GenderEntity(gender.GenderType, gender.Name,
                                              null,
                                              clothesInformationEntities)).
            ToList();
    }
}