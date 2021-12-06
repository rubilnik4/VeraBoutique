using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Images;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes
{
    /// <summary>
    /// Сущность изображения
    /// </summary>
    public interface IClothesImageEntity : IClothesImageBase, IEntityModel<Guid>
    {
        /// <summary>
        /// Сущность одежды
        /// </summary>
        ClothesEntity? Clothes { get; }
    }
}