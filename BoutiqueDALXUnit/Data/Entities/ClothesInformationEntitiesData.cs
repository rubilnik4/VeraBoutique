using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesType;
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
        public static List<ClothesInformationEntity> ClothesInformationEntities =>
            ClothesData.ClothesInformationDomains.
            Select(clothesInformation =>
                new ClothesInformationEntity(clothesInformation, clothesInformation.Description,
                                             new GenderEntity(clothesInformation.Gender.GenderType, clothesInformation.Gender.Name),
                                             GetClothesTypeEntity(clothesInformation.ClothesType), 
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

        /// <summary>
        /// Получить сущность типа одежды
        /// </summary>
        private static ClothesTypeEntity GetClothesTypeEntity(IClothesTypeDomain clothesTypeDomain) =>
             new ClothesTypeEntity(clothesTypeDomain.Name, clothesTypeDomain.Name,
                                   new CategoryEntity(clothesTypeDomain.CategoryDomain.Name),
                                   Enumerable.Empty<ClothesInformationEntity>(),
                                   Enumerable.Empty<ClothesTypeGenderCompositeEntity>());
    }
}