using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность одежды с цветом
    /// </summary>
    public interface IClothesColorCompositeEntity : IEntityModel<(int, string)>
    {
        /// <summary>
        /// Идентификатор одежды
        /// </summary>
        int ClothesId { get; }

        /// <summary>
        /// Идентификатор цвета
        /// </summary>
        string ColorName { get; }

        /// <summary>
        /// Одежда. Информация
        /// </summary>
        ClothesFullEntity? Clothes { get; }

        /// <summary>
        /// Цвет одежды
        /// </summary>
        ColorEntity? ColorClothes { get; }
    }
}