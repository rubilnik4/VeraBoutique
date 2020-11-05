using System.Collections.Generic;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Вид одежды. Сущность базы данных
    /// </summary>
    public interface IClothesTypeShortEntity: IClothesTypeEntity
    {
        /// <summary>
        /// Связующая сущность пола и вида одежды
        /// </summary>
        ClothesTypeGenderCompositeEntity? ClothesTypeGender { get; }
    }
}