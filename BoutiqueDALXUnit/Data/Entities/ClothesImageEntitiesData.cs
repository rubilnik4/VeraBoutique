using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDALXUnit.Data.Entities
{
    /// <summary>
    /// Данные сущностей изображений одежды
    /// </summary>
    public static class ClothesImageEntitiesData
    {
        /// <summary>
        /// Сущности информации об одежде
        /// </summary>
        public static IReadOnlyCollection<ClothesImageEntity> ClothesImageEntities =>
            ClothesImageData.ClothesImageDomains.
            Select(clothesImage => new ClothesImageEntity(clothesImage)).
            ToList();
    }
}