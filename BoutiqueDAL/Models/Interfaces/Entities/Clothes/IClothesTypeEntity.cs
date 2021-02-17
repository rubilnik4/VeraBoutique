using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes
{
    /// <summary>
    /// Вид одежды. Сущность базы данных
    /// </summary>
    public interface IClothesTypeEntity: IClothesTypeBase, IEntityModel<string>
    {
        /// <summary>
        /// Связующая сущность категории одежды
        /// </summary>
        CategoryEntity? Category { get; }

        /// <summary>
        /// Связующие сущности категории и одежды
        /// </summary>
        IReadOnlyCollection<ClothesEntity>? Clothes { get; }
    }
}