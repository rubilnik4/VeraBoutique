using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes
{
    /// <summary>
    /// Вид одежды. Сущность базы данных
    /// </summary>
    public interface IClothesTypeEntity: IClothesType, IEntityModel<string>
    {
        /// <summary>
        /// Идентификатор связующей сущности категории одежды
        /// </summary>
        string? CategoryName { get; }

        /// <summary>
        /// Связующая сущность категории одежды
        /// </summary>
        CategoryEntity? CategoryEntity { get; }

        /// <summary>
        /// Связующие сущности пола и вида одежды
        /// </summary>
        IReadOnlyCollection<ClothesTypeGenderEntity>? ClothesTypeGenderEntities { get; }
    }
}