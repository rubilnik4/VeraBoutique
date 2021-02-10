using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;

namespace BoutiqueDALXUnit.Data.Entities
{
    /// <summary>
    /// Данные сущностей цвета одежды
    /// </summary>
    public class ColorEntityData
    {
        /// <summary>
        /// Сущности цвета одежды
        /// </summary>
        public static IReadOnlyCollection<ColorEntity> ColorEntities =>
            ColorData.ColorDomains.
            Select(GetColorEntity).
            ToList();

        /// <summary>
        /// Получить сущность цвета одежды
        /// </summary>
        public static ColorEntity GetColorEntity(IColorDomain color) =>
             new (color);
    }
}