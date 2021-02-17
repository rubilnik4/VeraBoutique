using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDALXUnit.Data.Entities
{
    /// <summary>
    /// Данные сущностей одежды
    /// </summary>
    public class ClothesShortEntitiesData
    {
        /// <summary>
        /// Сущности одежды
        /// </summary>
        public static IReadOnlyCollection<ClothesEntity> ClothesShortEntities =>
            ClothesData.ClothesShortDomains.
            Select(clothesShort => new ClothesEntity(clothesShort)).
            ToList();
    }
}