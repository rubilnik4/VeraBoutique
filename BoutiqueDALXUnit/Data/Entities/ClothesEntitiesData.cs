using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
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
            ClothesData.ClothesMainDomains.
            Select(clothes =>
                new ClothesEntity(clothes,
                                  new GenderEntity(clothes.Gender),
                                  GetClothesTypeEntity(clothes.ClothesType),
                                  GetClothesColorCompositeEntities(clothes.Colors, clothes.Id),
                                  GetClothesSizeGroupCompositeEntities(clothes.SizeGroups, clothes.Id))).
            ToList();

        /// <summary>
        /// Получить связующие сущности одежды и цвета
        /// </summary>
        private static IEnumerable<ClothesColorCompositeEntity> GetClothesColorCompositeEntities(IEnumerable<IColorDomain> colorClothesDomains,
                                                                                                 int clothesId) =>
            colorClothesDomains.
            Select(colorClothesDomain => new ClothesColorCompositeEntity(clothesId, colorClothesDomain.Name,
                                                                         null, new ColorEntity(colorClothesDomain)));

        /// <summary>
        /// Получить связующие сущности размера и цвета
        /// </summary>
        private static IEnumerable<ClothesSizeGroupCompositeEntity> GetClothesSizeGroupCompositeEntities(IEnumerable<ISizeGroupMainDomain> sizeGroupDomains,
                                                                                                         int clothesId) =>
            sizeGroupDomains.
            Select(sizeGroupDomain => new ClothesSizeGroupCompositeEntity(clothesId, sizeGroupDomain.Id, 
                                                                          null, SizeGroupEntitiesData.GetSizeGroupEntity(sizeGroupDomain)));

        /// <summary>
        /// Получить сущность типа одежды
        /// </summary>
        private static ClothesTypeEntity GetClothesTypeEntity(IClothesTypeDomain clothesType) =>
             new (clothesType);
    }
}