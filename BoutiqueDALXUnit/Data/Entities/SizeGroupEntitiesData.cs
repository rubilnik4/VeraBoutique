using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;

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
            SizeGroupData.SizeGroupMainDomains.
            Select(GetSizeGroupEntity).
            ToList();

        /// <summary>
        /// Получить сущность группы размеров
        /// </summary>
        public static SizeGroupEntity GetSizeGroupEntity(ISizeGroupMainDomain sizeGroup) =>
             new (sizeGroup, GetSizeGroupComposite(sizeGroup.Id, sizeGroup.Sizes));

        /// <summary>
        /// Получить связующую сущность группы размеров
        /// </summary>
        public static IEnumerable<SizeGroupCompositeEntity> GetSizeGroupComposite(int sizeGroupId, IEnumerable<ISizeDomain> sizes) =>
            sizes.Select(size => new SizeGroupCompositeEntity(size.Id, sizeGroupId, new SizeEntity(size), null));
    }
}