using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDALXUnit.Data.Entities.Clothes
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
        /// Сущности типа пола с категорией
        /// </summary>
        public static IReadOnlyCollection<GenderEntity> GenderCategoryEntities =>
            GenderData.GenderCategoryDomains.
            Select(genderCategoryDomain => 
                new GenderEntity(genderCategoryDomain,
                                 CategoryEntitiesData.GetCategoryComposite(genderCategoryDomain.GenderType, genderCategoryDomain.Categories))).
            ToList();

        /// <summary>
        /// Получить сущности типа пола c информацией об одежде
        /// </summary>
        public static IReadOnlyCollection<GenderEntity> GetGenderEntitiesWithClothes(IReadOnlyCollection<GenderEntity> genderEntities,
                                                                                     IReadOnlyCollection<ClothesEntity> clothesInformationEntities) =>
            genderEntities.
            Select(gender => new GenderEntity(gender.GenderType, gender.Name, null, clothesInformationEntities)).
            ToList();

       

    }
}