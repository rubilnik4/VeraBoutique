﻿using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
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
        public static List<ClothesEntity> ClothesInformationEntities =>
            ClothesData.ClothesInformationDomains.
            Select(clothesInformation =>
                new ClothesEntity(clothesInformation, clothesInformation.Description,
                                             new GenderEntity(clothesInformation.Gender.GenderType, clothesInformation.Gender.Name),
                                             GetClothesTypeEntity(clothesInformation.ClothesTypeFull), 
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
        private static ClothesTypeEntity GetClothesTypeEntity(IClothesTypeFullDomain clothesTypeFullDomain) =>
             new ClothesTypeEntity(clothesTypeFullDomain.Name, clothesTypeFullDomain.Name,
                                   new CategoryEntity(clothesTypeFullDomain.Category.Name),
                                   Enumerable.Empty<ClothesEntity>(),
                                   Enumerable.Empty<ClothesTypeGenderCompositeEntity>());
    }
}