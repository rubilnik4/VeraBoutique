using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Owns;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes
{
    /// <summary>
    /// Одежда. Информация. Сущность базы данных
    /// </summary>
    public interface IClothesEntity : IClothesBase, IEntityModel<int>
    {
        /// <summary>
        /// Изображение
        /// </summary>
        IReadOnlyCollection<ClothesImageEntity>? Images { get; }

        /// <summary>
        /// Связующая сущность типа одежды
        /// </summary>
        GenderEntity? Gender { get; }

        /// <summary>
        /// Связующая сущность типа одежды
        /// </summary>
        ClothesTypeEntity? ClothesType { get; }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        IReadOnlyCollection<ClothesColorCompositeEntity>? ClothesColorComposites { get; }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        IReadOnlyCollection<ClothesSizeGroupCompositeEntity>? ClothesSizeGroupComposites { get; }
    }
}