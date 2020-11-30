using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;

namespace BoutiqueDALXUnit.Data.Entities
{
    /// <summary>
    /// Данные сущностей информации об одежде
    /// </summary>
    public class ClothesEntitiesData
    {
        /// <summary>
        /// Сущности информации об одежде
        /// </summary>
        public static IReadOnlyCollection<ClothesEntity> ClothesEntities =>
            ClothesData.ClothesDomains.
            Select(clothes =>
                new ClothesEntity(clothes,
                                  new GenderEntity(clothes.Gender.GenderType, clothes.Gender.Name),
                                  GetClothesTypeEntity(clothes.ClothesTypeShort),
                                  GetClothesColorCompositeEntities(clothes.Colors, clothes.Id),
                                  GetClothesSizeGroupCompositeEntities(clothes.SizeGroups, clothes.Id))).
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
        private static ClothesTypeEntity GetClothesTypeEntity(IClothesTypeShortDomain clothesTypeShort) =>
             new ClothesTypeEntity(clothesTypeShort, null, null);
    }
}