using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;

namespace BoutiqueDALXUnit.Data.Entities
{
    /// <summary>
    /// Данные сущностей информации об одежде
    /// </summary>
    public class ClothesInformationEntitiesData
    {
        /// <summary>
        /// Сущности информации об одежде
        /// </summary>
        public static IList<ClothesInformationEntity> ClothesInformationEntities =>
            ClothesData.ClothesInformationDomains.
            Select(clothesInformation =>
                new ClothesInformationEntity(clothesInformation, clothesInformation.Description,
                                             new ClothesTypeEntity(clothesInformation.ClothesType.Name), 
                                             GetClothesColorCompositeEntities(clothesInformation.Colors, clothesInformation.Id),
                                             GetClothesSizeGroupCompositeEntities(clothesInformation.SizeGroups,
                                                                                  clothesInformation.Id))).
            ToList();

        /// <summary>
        /// Получить связующие сущности одежды и цвета
        /// </summary>
        private static IEnumerable<ClothesColorCompositeEntity> GetClothesColorCompositeEntities(IEnumerable<IColorClothesDomain> colorClothesDomains,
                                                                                               int clothesId) =>
            colorClothesDomains.
            Select(colorClothesDomain => new ClothesColorCompositeEntity(clothesId, colorClothesDomain.Name,
                                                                         null, new ColorClothesEntity(colorClothesDomain.Name)));

        /// <summary>
        /// Получить связующие сущности размера и цвета
        /// </summary>
        private static IEnumerable<ClothesSizeGroupCompositeEntity> GetClothesSizeGroupCompositeEntities(IEnumerable<ISizeGroupDomain> sizeGroupDomains,
                                                                                                         int clothesId) =>
            sizeGroupDomains.
            Select(sizeGroupDomain => new ClothesSizeGroupCompositeEntity(clothesId, sizeGroupDomain.ClothesSizeType, 
                                                                          sizeGroupDomain.SizeNormalize,
                                                                          null, SizeGroupEntitiesData.GetSizeGroupEntity(sizeGroupDomain)));
    }
}