using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

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
        public static List<GenderEntity> GenderEntities =>
            GenderData.GetGendersDomain().
            Select(genderDomain => new GenderEntity(genderDomain.GenderType, genderDomain.Name)).
            ToList();

        /// <summary>
        /// Получить сущности типа пола c видом одежды
        /// </summary>
        public static List<GenderEntity> GetGenderEntitiesWithClothesType(IReadOnlyCollection<GenderEntity> genderEntities,
                                                                          IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities) =>
            genderEntities.
            Select(gender => new GenderEntity(gender.GenderType, gender.Name,
                                              ClothesTypeEntitiesData.GetClothesTypeGenderEntity(gender, clothesTypeEntities),
                                              Enumerable.Empty<ClothesInformationEntity>())).
            ToList();
    }
}