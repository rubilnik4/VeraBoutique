using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes
{
    /// <summary>
    /// Одежда. Информация. Сущность базы данных
    /// </summary>
    public interface IClothesInformationEntity : IClothesInformation, IClothesShortEntity
    {
        /// <summary>
        /// Идентификатор связующей сущности типа одежды
        /// </summary>
        string? ClothesTypeName { get; }

        /// <summary>
        /// Связующая сущность типа одежды
        /// </summary>
        ClothesTypeEntity? ClothesTypeEntity { get; }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        public IReadOnlyCollection<ClothesColorCompositeEntity> ClothesColorCompositeEntities { get; }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        public IReadOnlyCollection<ClothesSizeGroupCompositeEntity> ClothesSizeGroupCompositeEntities { get; }
    }
}