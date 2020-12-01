using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesEntities
{
    /// <summary>
    /// Одежда. Базовая сущность базы данных
    /// </summary>
    public interface IClothesShortEntity : IClothesMain, IEntityModel<int>
    {
        /// <summary>
        /// Идентификатор связующей сущности типа одежды
        /// </summary>
        GenderType GenderType { get; }

        /// <summary>
        /// Идентификатор связующей сущности типа одежды
        /// </summary>
        string ClothesTypeName { get; }
    }
}