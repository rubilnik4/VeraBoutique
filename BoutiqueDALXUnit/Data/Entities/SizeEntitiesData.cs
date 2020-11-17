using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDALXUnit.Data.Entities
{
    /// <summary>
    /// Данные сущностей размера
    /// </summary>
    public class SizeEntitiesData
    {
        /// <summary>
        /// Сущности группы размеров
        /// </summary>
        public static List<SizeEntity> SizeEntities =>
            SizeData.SizeDomain.
            Select(GetSizeEntity).
            ToList();

        /// <summary>
        /// Получить сущность группы размеров
        /// </summary>
        public static SizeEntity GetSizeEntity(ISizeDomain size) =>
             new SizeEntity(size.SizeType, size.SizeName);
    }
}