using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Вид одежды. Базовая сущность базы данных
    /// </summary>
    public interface IClothesTypeShortEntity: IClothesType, IEntityModel<string>
    {
        /// <summary>
        /// Идентификатор связующей сущности категории одежды
        /// </summary>
        string CategoryName { get; }

        /// <summary>
        /// Связующая сущность категории одежды
        /// </summary>
        CategoryEntity? Category { get; }
    }
}