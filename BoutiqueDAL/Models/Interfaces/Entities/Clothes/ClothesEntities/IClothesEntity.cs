using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesEntities
{
    /// <summary>
    /// Одежда. Информация. Сущность базы данных
    /// </summary>
    public interface IClothesEntity : IClothesMain, IClothesShortEntity
    {
        /// <summary>
        /// Идентификатор связующей сущности типа одежды
        /// </summary>
        GenderType GenderType { get; }

        /// <summary>
        /// Связующая сущность типа одежды
        /// </summary>
        GenderEntity? Gender { get; }

        /// <summary>
        /// Идентификатор связующей сущности типа одежды
        /// </summary>
        string ClothesTypeName { get; }

        /// <summary>
        /// Связующая сущность типа одежды
        /// </summary>
        ClothesTypeFullEntity? ClothesType { get; }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        public IReadOnlyCollection<ClothesColorCompositeEntity>? ClothesColorComposites { get; }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        public IReadOnlyCollection<ClothesSizeGroupCompositeEntity>? ClothesSizeGroupComposites { get; }
    }
}