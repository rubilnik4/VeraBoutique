using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities;

namespace BoutiqueDALXUnit.Data.Entities
{
    /// <summary>
    /// Данные сущностей группы размеров
    /// </summary>
    public class SizeGroupEntitiesData
    {
        /// <summary>
        /// Сущности группы размеров
        /// </summary>
        public static IReadOnlyCollection<SizeGroupEntity> SizeGroupEntities =>
            SizeGroupData.SizeGroupDomain.
            Select(GetSizeGroupEntity).
            ToList();

        /// <summary>
        /// Получить сущность группы размеров
        /// </summary>
        public static SizeGroupEntity GetSizeGroupEntity(ISizeGroupDomain sizeGroup) =>
             new SizeGroupEntity(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize,
                                 GetSizeGroupComposite(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize, sizeGroup.Sizes));

        /// <summary>
        /// Получить связующую сущность группы размеров
        /// </summary>
        public static IEnumerable<SizeGroupCompositeEntity> GetSizeGroupComposite(ClothesSizeType clothesSizeType, int sizeNormalize,
                                                                                  IEnumerable<ISizeDomain> sizes) =>
            sizes.Select(size => new SizeGroupCompositeEntity(size.SizeType, size.SizeName, clothesSizeType, sizeNormalize,
                                                              new SizeEntity(size.SizeType, size.SizeName),
                                                              null));
    }
}