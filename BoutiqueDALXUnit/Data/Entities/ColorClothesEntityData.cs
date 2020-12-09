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
    public class ColorClothesEntityData
    {
        /// <summary>
        /// Сущности цвета одежды
        /// </summary>
        public static IReadOnlyCollection<ColorClothesEntity> ColorClothesEntities =>
            ColorClothesData.ColorClothesDomain.
            Select(GetColorClothesEntity).
            ToList();

        /// <summary>
        /// Получить сущность цвета одежды
        /// </summary>
        public static ColorClothesEntity GetColorClothesEntity(IColorDomain color) =>
             new ColorClothesEntity(color.Name);
    }
}