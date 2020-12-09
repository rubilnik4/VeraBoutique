using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Вид одежды. Базовая сущность базы данных
    /// </summary>
    public interface IClothesTypeShortEntity: IClothesTypeShortBase, IEntityModel<string>
    {
        /// <summary>
        /// Идентификатор связующей сущности категории одежды
        /// </summary>
        string CategoryName { get; }

        /// <summary>
        /// Связующие сущности категории и одежды
        /// </summary>
        IReadOnlyCollection<ClothesEntity>? Clothes { get; }
    }
}