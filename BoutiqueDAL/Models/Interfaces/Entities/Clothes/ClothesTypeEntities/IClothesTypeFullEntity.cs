using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Вид одежды. Полная информация. Сущность базы данных
    /// </summary>
    public interface IClothesTypeFullEntity: IClothesTypeEntity
    {
        /// <summary>
        /// Связующие сущности категории и одежды
        /// </summary>
        IReadOnlyCollection<ClothesEntity>? Clothes { get; }

        /// <summary>
        /// Связующие сущности пола и вида одежды
        /// </summary>
        IReadOnlyCollection<ClothesTypeGenderCompositeEntity>? ClothesTypeGenderComposites { get; }
    }
}